$PSScriptRoot
docker run -it --rm -d -p 8080:80 --name web -v "$($PSScriptRoot)\front-end:/usr/share/nginx/html" nginx
docker run -it --rm -d -p 8081:80 --name tem-wc -v "$($PSScriptRoot)\temperature\temp-wc\widget:/usr/share/nginx/html" nginx