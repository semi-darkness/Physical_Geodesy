gmt begin Geoid_height png
gmt xyz2grd Geoid_height.txt -Ggrd1.grd -I1d -R120/128/20/28
gmt grdsample grd1.grd -Ggrd2.grd -I0.2d
gmt grd2cpt  grd2.grd -Cjet -Z
gmt grdimage -R120/128/20/28 -JCyl_stere/124/24/50c -Ba30 grd2.grd -I+d -B+tGeoid_height
gmt colorbar  -B2
gmt coast -R120/128/20/28 -JCyl_stere/124/24/50c -Baf -W1p,black -A5000 
gmt end show
del grd1.grd grd2.grd
