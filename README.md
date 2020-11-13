![]()
# SSO - APIRestFull

- Integrado con [Swagger](https://swagger.io/) para documentación.
- Api con seguridad [JWT](https://jwt.io/)

## Restaurar Paquetes NuGet
Para restaurar los paquete NuGet debemos abrir nuestra solución en VisualStudio, después dar click derecho sobre "Solución" y presionar la opción restaurar paquetes NuGet.

## Generar el proyecto
Se debe dar publish al proyecto en dónde lo queramos dejar y después lo colocamos en un servidor de IIS. La carpeta donde esta el proyecto compilado es: `bin\Release\PublishOutput`, si no publicar directamente en el computador.

## Configuraciones
Las configuraciones se encuentran en el web.config donde:
```sh
"PublicServerIpToSwagger" #Ip pública del servidor requerida para que swagger funcione correctamente.
"ConnectionString"        #Valor para la conexión a Base de datos.
```


![](./logos/swaggerLogo.png)
## Probar en Api [Swagger]
Por ahora no está disponible, ya que se debe integrar la autentificación de OAuth.

[Sólo se puede ver la documentación generada]

Se recomienda por ahora probar con [Postman](https://www.getpostman.com).
