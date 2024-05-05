# TwillioSms

## Descripción
Este es un proyecto que utiliza Twilio para enviar mensajes SMS a pacientes. Los datos de los pacientes se almacenan en MongoDB y se consultan a través de una API.

## Instalación
1. Clona este repositorio.
2. Ejecuta `dotnet restore`.
3. Copia `appsettings.json.example` a `appsettings.json` y rellena tus credenciales de Twilio y MongoDB.

## Uso
El servicio se ejecuta en segundo plano y envía mensajes SMS a los pacientes cuyas citas están programadas para el día siguiente. Los mensajes se envían cada `INTERVALO_ENVIO` horas.

## Contribuciones
Las contribuciones son bienvenidas. Por favor, abre un problema o una solicitud de extracción.

## Licencia
Este proyecto está licenciado bajo la licencia MIT.
