function inflator(filename, out)
% filename='input_sketches/fish.png';
% out='fish_mine.obj';
% color_out = 'color-fish_mine.txt';
% % Reading the input (RGB) image
image = imread(filename);

% Resizing the image to a fixed height of 300px
[x,y,z] = size(image);
image = imresize(image, [225, 225*(y/x)]);
% figure;
% imshow(image);
% % RGB to Grayscale
gray_image = rgb2gray(image);

% % Grayscale to Binary
binary_image = imbinarize(gray_image);

% % Boundary Finding

% % Change to HSV color space
hsv_image = rgb2hsv(image);
sat_image = hsv_image(:,:,2);

% % Adaptive thresholding on saturation values
min_val = min(sat_image(:));
thres_image = sat_image <= min_val;

% % Create a kernel of size 3
se = strel('disk',3);

% % Morphological open and close operations
morph_close = imclose(thres_image,se);
morph_open = imopen(morph_close,se);

% % Dilate the image
image_outline = imdilate(morph_open,se);

% % Get the Boundary
bound = boundarymask(image_outline);

% % Flood filling to fill small holes in the region image 
filled_binary = imfill(binary_image, 'holes');
binary_image = filled_binary;

% % Get regions of the image
region = bwlabel(binary_image);
% figure;
% imshow(region);

R = region(1, 1);

% % Get boundary mask
boundary = boundarymask(binary_image);
% figure;
% imshow(boundary);

% %  Distance map formation for 2D to 3D transformation

distance = bwdist(boundary);

for i=1:size(distance, 1)
    for j=1:size(distance, 2)
        if(region(i, j) == R)
            distance(i, j) = NaN;
        end
    end
end

% % Plotting the distance map.
% figure;
% imagesc(distance); axis equal;

height = distance;
max_height = max(distance(:));

for i = 1:size(distance, 1)
    for j = 1:size(distance, 2)
        a = max_height - distance(i, j);
        height(i, j) = sqrt(max_height*max_height - a*a);
    end
end

% % Create threshold
threshold = 0.3*max(height(:));

% % Thresholding for better top and bottom stitiching
for i=1:size(distance, 1)
    for j=1:size(distance, 2)
        if(height(i, j) < threshold)
            height(i, j) = NaN;
        end
    end
end

x = 1:size(height, 1);
y = 1:size(height, 2);
[X, Y] = meshgrid(y, x);

% Combine top and bottom with a shift to compensate for thresholding
% % Texture map the now formed 3D surface using the information from the given 2D sketch

% figure;
% shading flat;
h = warp([X;X], [Y;Y], [height - (1.2*threshold); -height + (1.2*threshold)], [image; image]);
% axis equal;

% % Exporting this 3D model as STL or OBJ file (which can now be imported
% % into Unity 3D.

save_obj(out, h.XData, h.YData, h.ZData, h.VertexNormals, h.CData);
