FROM node:16 as builder
WORKDIR /app
COPY ./energy_consumption_frontend_app/package*.json /app/
RUN npm install
COPY ./energy_consumption_frontend_app /app/
RUN npm run build


FROM nginx:1.17-alpine
RUN apk --no-cache add curl
COPY ./energy_consumption_frontend_app/nginx.conf /etc/nginx/nginx.conf
COPY --from=builder /app/build/ /usr/share/nginx/html
EXPOSE 80
