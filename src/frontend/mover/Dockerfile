# Step 1: Build Angular
FROM node:latest as build
ARG VITE_APP_GATEWAY_URL
ENV VITE_APP_GATEWAY_URL=$VITE_APP_GATEWAY_URL

RUN mkdir /app
WORKDIR /app

# Add the source code to app
COPY ./ /app

# Install all the dependencies && Generate the build of the application
RUN npm install && npm run build

# Stage 2: Serve app with nginx server
# Use official nginx image as the base image
FROM nginx:latest

# Copy the build output to replace the default nginx contents.
COPY --from=build /app/dist /usr/share/nginx/html

# Copy NGINX file
COPY docker/nginx.conf /etc/nginx

# Expose port 80
EXPOSE 80
EXPOSE 443