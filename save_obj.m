function save_obj(filename, x, y, z, normals, vcolor)

if (ischar(filename)==0)
    error( 'Invalid filename');
end

% Open for writing in ascii mode
fid = fopen(filename,'w');

if (fid == -1)
    error( sprintf('Unable to write to %s',filename) );
end

title_str = sprintf('Created the files on %s',datestr(now));
disp(title_str);

% fprintf(fid,'solid %s\r\n',title_str);

nfacets = 0;
print_index = 0;

len = size(x);
hash_print = -1*ones(len(1), len(2));

for i=1:(size(z,1)-1)
    for j=1:(size(z,2)-1)
        current_face_components = [-1 -1 -1];

        p1 = [x(i,j)     y(i,j)     z(i,j)];
        np1 = [normals(i,j,1) normals(i,j,2) normals(i,j,3)];
        c1 = [vcolor(i,j,1) vcolor(i,j,2) vcolor(i,j,3)];

        p2 = [x(i,j+1)   y(i,j+1)   z(i,j+1)];
        np2 = [normals(i,j+1,1) normals(i,j+1,2) normals(i,j+1,3)];
        c2 = [vcolor(i,j+1,1) vcolor(i,j+1,2) vcolor(i,j+1,3)];

        p3 = [x(i+1,j+1) y(i+1,j+1) z(i+1,j+1)];
        np3 = [normals(i+1,j+1,1) normals(i+1,j+1,2) normals(i+1,j+1,3)];
        c3 = [vcolor(i+1,j+1,1) vcolor(i+1,j+1,2) vcolor(i+1,j+1,3)];

        if ~any( isnan(p1) | isnan(p2) | isnan(p3) )
            if (hash_print(i, j) == -1)
                print_index = print_index + 1;
                hash_print(i, j) = print_index;
                fprintf(fid,'v %.7E %.7E %.7E\r\n', p1);
                % fprintf(fid,'vn %.7E %.7E %.7E\r\n', np1);
                fprintf(fid,'c %.7E %.7E %.7E\r\n', c1);
                current_face_components(1) = print_index;
            else
                current_face_components(1) = hash_print(i, j);
            end

            if (hash_print(i, j+1) == -1)
                print_index = print_index + 1;
                hash_print(i, j+1) = print_index;
                fprintf(fid,'v %.7E %.7E %.7E\r\n', p2);
                % fprintf(fid,'vn %.7E %.7E %.7E\r\n', np2);
                fprintf(fid,'c %.7E %.7E %.7E\r\n', c2);
                current_face_components(2) = print_index;
            else
                current_face_components(2) = hash_print(i, j+1);
            end

            if (hash_print(i+1, j+1) == -1)
                print_index = print_index + 1;
                hash_print(i+1, j+1) = print_index;
                fprintf(fid,'v %.7E %.7E %.7E\r\n', p3);
                % fprintf(fid,'vn %.7E %.7E %.7E\r\n', np3);
                fprintf(fid,'c %.7E %.7E %.7E\r\n', c3);
                current_face_components(3) = print_index;
            else
                current_face_components(3) = hash_print(i+1, j+1);
            end
            if (sign(p1(3)) + sign(p2(3)) + sign(p3(3)) <= -2)
                fprintf(fid,'f %d %d %d\r\n', current_face_components(3), current_face_components(2), current_face_components(1));
            else
                fprintf(fid,'f %d %d %d\r\n', current_face_components(1), current_face_components(2), current_face_components(3));
            end
            nfacets = nfacets + 1;
        end
        
        current_face_components = [-1 -1 -1];

        p1 = [x(i+1,j+1) y(i+1,j+1) z(i+1,j+1)];
        np1 = [normals(i+1,j+1,1) normals(i+1,j+1,2) normals(i+1,j+1,3)];
        c1 = [vcolor(i+1,j+1,1) vcolor(i+1,j+1,2) vcolor(i+1,j+1,3)];

        p2 = [x(i+1,j)   y(i+1,j)   z(i+1,j)];
        np2 = [normals(i+1,j,1) normals(i+1,j,2) normals(i+1,j,3)];
        c2 = [vcolor(i+1,j,1) vcolor(i+1,j,2) vcolor(i+1,j,3)];

        p3 = [x(i,j)     y(i,j)     z(i,j)];
        np3 = [normals(i,j,1) normals(i,j,2) normals(i,j,3)];
        c3 = [vcolor(i,j,1) vcolor(i,j,2) vcolor(i,j,3)];

        if ~any( isnan(p1) | isnan(p2) | isnan(p3) )
            if (hash_print(i+1, j+1) == -1)
                print_index = print_index + 1;
                hash_print(i+1, j+1) = print_index;
                fprintf(fid,'v %.7E %.7E %.7E\r\n', p1);
                % fprintf(fid,'vn %.7E %.7E %.7E\r\n', np1);
                fprintf(fid,'c %.7E %.7E %.7E\r\n', c1);
                current_face_components(1) = print_index;
            else
                current_face_components(1) = hash_print(i+1, j+1);
            end

            if (hash_print(i+1, j) == -1)
                print_index = print_index + 1;
                hash_print(i+1, j) = print_index;
                fprintf(fid,'v %.7E %.7E %.7E\r\n', p2);
                % fprintf(fid,'vn %.7E %.7E %.7E\r\n', np2);
                fprintf(fid,'c %.7E %.7E %.7E\r\n', c2);
                current_face_components(2) = print_index;
            else
                current_face_components(2) = hash_print(i+1, j);
            end

            if (hash_print(i, j) == -1)
                print_index = print_index + 1;
                hash_print(i, j) = print_index;
                fprintf(fid,'v %.7E %.7E %.7E\r\n', p3);
                % fprintf(fid,'vn %.7E %.7E %.7E\r\n', np3);
                fprintf(fid,'c %.7E %.7E %.7E\r\n', c3);
                current_face_components(3) = print_index;
            else
                current_face_components(3) = hash_print(i, j);
            end
            if (sign(p1(3)) + sign(p2(3)) + sign(p3(3)) <= -2)
                fprintf(fid,'f %d %d %d\r\n', current_face_components(3), current_face_components(2), current_face_components(1));
            else
                fprintf(fid,'f %d %d %d\r\n', current_face_components(1), current_face_components(2), current_face_components(3));
            end
            nfacets = nfacets + 1;
        end
        
    end
end

fclose(fid);

disp( sprintf('Wrote %d facets',nfacets) );