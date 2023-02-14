# DEMO - Culqi Net/NetCore + Checkout V4 + Culqi 3DS

La demo integra Culqi Net/NetCore, Checkout V4 , Culqi 3DS y es compatible con la v2.0 del Culqi API, con esta demo podrás generar tokens, cargos, clientes, cards.

## Requisitos

* Net 6.0+
* Visual Studio 2022
* culqinet.dll (se genera a partir de la librería culqi-net)
* Afiliate [aquí](https://afiliate.culqi.com/).
* Si vas a realizar pruebas obtén tus llaves desde [aquí](https://integ-panel.culqi.com/#/registro), si vas a realizar transacciones reales obtén tus llaves desde [aquí](https://panel.culqi.com/#/registro) (1).

> Recuerda que para obtener tus llaves debes ingresar a tu CulqiPanel > Desarrollo > ***API Keys***.

![alt tag](http://i.imgur.com/NhE6mS9.png)

> Recuerda que las credenciales son enviadas al correo que registraste en el proceso de afiliación.

## Configuración

Dentro del archivo **card.cs** coloca tus llaves pk y sk.

## Inicializar la demo

Ejecutar la demo desde Visual Studio 2022.

## Probar la demo

Para poder visualizar el frontend de la demo ingresar a la siguiente URL:

- Para probar cargos: `https://localhost:7165/vista/index.html`
- Para probar creación de cards: `https://localhost:7165/vista/index-card.html`
