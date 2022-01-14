gmt begin disturbing_potential png
gmt xyz2grd disturbing_potential.txt -Ggrd1.grd -I1d -R120/128/20/28
gmt grdsample grd1.grd -Ggrd2.grd -I0.2d
gmt grd2cpt  grd2.grd -Cjet -Z
gmt grdimage -R120/128/20/28 -JCyl_stere/124/24/50c -Ba30 grd2.grd -I+d -B+tdisturbing_potential
gmt colorbar  -B25
gmt coast -R120/128/20/28 -JCyl_stere/124/24/50c -Baf -W1p,black -A5000 
gmt end show
del grd1.grd grd2.grd
