a=load('C:\Users\lcc\Desktop\2019302141346PG_2\ConsoleApplication1\ConsoleApplication1\bin\Debug\间隔为1度 高度为0.txt');
t=a(2,2)-a(1,2);
mini=min(a(:,3));
maxi=max(a(:,3));
min1=min(a(:,1));
max1=max(a(:,1));
min2=min(a(:,2));
max2=max(a(:,2));
jiange=(maxi-mini)/50;
levels = mini:jiange:maxi;%调整中间参数可以调节梯度
b1=1+(max2-min2)/t;
b2=1+(max1-min1)/t;
b=reshape(a(:,3),[b1,b2]);
b=b';
worldmap([min1 max1],[min2 max2]);
colorbar
contourfm(min1:t:max1,min2:t:max2,b,levels,'LineStyle','none')
geoshow("D:\EGM2008\bou2_4l.shp")
load coastlines
geoshow(coastlat,coastlon,'color','k')
title("中国高程异常")
saveas( gcf, 'C:\Users\lcc\Desktop\save.jpg'); 
