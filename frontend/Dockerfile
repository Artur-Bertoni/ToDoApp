# Etapa 1: Build
FROM node:18 AS build
WORKDIR /app

# Instala dependências
COPY package*.json ./
RUN npm install

# Copia todo o código-fonte e faz o build
COPY . ./
RUN npm run build

# Etapa 2: Servidor NGINX para servir o frontend
FROM nginx:stable-alpine
COPY --from=build /app/dist /usr/share/nginx/html

# Exposição da porta do frontend
EXPOSE 80

# Comando para rodar o NGINX
CMD ["nginx", "-g", "daemon off;"]
