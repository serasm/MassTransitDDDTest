# MassTransitDDDTest
 
You need to install DockerDesktop

To use docker command you have to be inside project directory, at the same level as docker-compose file.
After that you can use "docker-compose up --build" command.

To clean you can use:
"docker system prune"
"docker volume prune"

From DockerDesktop you can use RabbitMQ or pgAdmin4 to look at DB.
To login into RabbitMq:
Login: guest
Password: guest

To login to pgAdmin 4 use:
Login: admin@admin.com
Password: postgres

To configure DB connection inside pgAdmin 4 please use:
1. Register Server
2. Set name
3. Inside connection tab set:
   Host name/address: db
   Username: postgres
   Password: postgres
