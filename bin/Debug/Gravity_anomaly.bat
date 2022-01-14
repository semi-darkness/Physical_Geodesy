gmt begin Gravity_anomaly png
gmt xyz2grd Gravity_anomaly.txt -Ggrd1.grd -I1d -R120/128/20/28
gmt grdsample grd1.grd -Ggrd2.grd -I0.2d
gmt grd2cpt  grd2.grd -Cjet -Z
gmt grdimage -R120/128/20/28 -JCyl_stere/124/24/50c -Ba30 grd2.grd -I+d -B+tGravity_anomaly
gmt colorbar  -B30
gmt coast -R120/128/20/28 -JCyl_stere/124/24/50c -Baf -W1p,black -A5000 
gmt end show
del grd1.grd grd2.grd
