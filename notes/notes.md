# Front-End

If you want a web server while you are working on the front-end, you can run:

`docker run -it --rm -d -p 8080:80 --name web -v C:\dev\path-tofolder\front-end:/usr/share/nginx/html nginx`


## Creating Custom Elements with Angular

1. Create an Angular project as normal.
2.  `  docker-compose -f .\docker-compose-dev.yml up -d`