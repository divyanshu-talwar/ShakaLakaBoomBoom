function saveobjmesh(name,x,y,z,normals)
% SAVEOBJMESH Save a x,y,z mesh as a Wavefront/Alias Obj file
% SAVEOBJMESH(fname,x,y,z,nx,ny,nz)
%     Saves the mesh to the file named in the string fname
%     x,y,z are equally sized matrices with coordinates.
%     nx,ny,nz are normal directions (optional)

  
  l=size(x,1); h=size(x,2);  

  n=zeros(l,h);
  
  fid=fopen(name,'w');
  nn=1;
  for i=1:l
    for j=1:h
      n(i,j)=nn; 
      if isnan(z(i, j)) || z(i, j) < 2
        continue
      end
      fprintf(fid, 'v %f %f %f\n',x(i,j),y(i,j),z(i,j)); 
      fprintf(fid, 'vn %f %f %f\n', normals(i, j, 1), normals(i, j, 2), normals(i, j, 3));
    %   fprintf(fid, 'vt %f %f\n',(i-1)/(l-1),(j-1)/(h-1)); 
    %   if (normals) fprintf(fid, 'vn %f %f %f\n', nx(i,j),ny(i,j),nz(i,j)); end
    
      nn=nn+1;
    end
  end
  fprintf(fid,'g mesh\n');
  
  for i=1:(l-1)
    for j=1:(h-1)
    %   if (normals)
    if isnan(z(i, j)) || z(i, j) < 2
        continue
    end
	% fprintf(fid,'f %d/%d/%d %d/%d/%d %d/%d/%d %d/%d/%d\n',n(i,j),n(i,j),n(i,j),n(i+1,j),n(i+1,j),n(i+1,j),n(i+1,j+1),n(i+1,j+1),n(i+1,j+1),n(i,j+1),n(i,j+1),n(i,j+1));
    %   else
  	fprintf(fid,'f %d/%d %d/%d %d/%d %d/%d\n',n(i,j),n(i,j),n(i+1,j),n(i+1,j),n(i+1,j+1),n(i+1,j+1),n(i,j+1),n(i,j+1));
    %   end
    end
  end
  fprintf(fid,'g\n\n');
  fclose(fid);
  