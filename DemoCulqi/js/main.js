import config, { customerInfo } from "./config/index.js";
import culqiConfig from "./config/checkout.js";
import "./config/culqi3ds.js";
import {
  generateChargeImpl,
  createCustomerImpl,
  createCardImpl,
  generateOrderImpl,
} from "./services/impl/index.js";
import * as selectors from "./helpers/selectors.js";

let jsonParams = {
  installments: paymenType === "cargo" ? true : false,
  orderId: paymenType === "cargo" ? await generarOrder() : '',
  buttonTex: paymenType === "cargo" ? '' : 'Guardar Tarjeta',
  amount : paymenType === "cargo" ? config.TOTAL_AMOUNT : ''
}

async function generarOrder(){
  const { statusCode, data } = await generateOrderImpl();
    console.log("El StatusCode es ", statusCode)
    console.log("La data es ", data.Content)
  if (data.StatusCode === 201) {
    console.log("Order",data);
      var objeto = JSON.parse(data.Content);
      console.log("El id es ", objeto.id);

    return objeto.id;
  } else {
    console.log('No se pudo obtener la orden');
  }
  return '';
}

culqiConfig(jsonParams);


const deviceId = await Culqi3DS.generateDevice();
if (!deviceId) {
  console.log("Ocurrio un error al generar el deviceID");
}

let tokenId,
  email,
  customerId = null;
window.addEventListener(
  "message",
  async function (event) {   
    if (event.origin === window.location.origin) {
      const { parameters3DS, error } = event.data;
      console.log("Llego addEventListener");
      if (parameters3DS) {
        let statusCode = null;
        let objResponse = null;
        if (paymenType === "cargo") {
          const responseCharge = await generateChargeImpl({
            deviceId,
            email,
            tokenId,
            parameters3DS,
          }); //2da llamada a cargo
          var objeto = JSON.parse(responseCharge.data.Content);
          objResponse = objeto.object;
          statusCode = responseCharge.data.StatusCode;          
          console.log(statusCode);
        } else {
          const responseCard = await createCardImpl({
            customerId,
            tokenId,
            deviceId,
            parameters3DS,
          }); //2da llamada a creacion de CARD, validacion de 1 sol
          var objeto = JSON.parse(responseCard.data.Content);
          objResponse = objeto.object;
          statusCode = responseCard.data.StatusCode;
        }

        if (statusCode === 201) {
          if (objResponse == "charge" || objResponse == "card") {
            $("#response_card").text("OPERACIÓN REALIZADA EXITOSAMENTE");
          }
          selectors.loadingElement.style.display = "none";
          Culqi3DS.reset();
        } else {
          selectors.paymentFailElement.style.display = "block";
          Culqi3DS.reset();
        }
      }

      if (error) {
        console.log("Ocurrio un error", error);
        selectors.loadingElement.style.display = "none";
      }
    }
  },
  false
);

window.culqi = async () => {
  console.log(paymenType);
  if (Culqi.token) {
    Culqi.close();
    tokenId = Culqi.token.id;
    email = Culqi.token.email;
    selectors.loadingElement.style.display = "block";

	let statusCode = null;
    let objResponse = null;
    if (paymenType === "cargo") {
      console.log("pagosss");
      const responseCharge = await generateChargeImpl({
        deviceId,
        email,
        tokenId,
      }); //1ra llamada a cargo
     
      console.log(responseCharge.data.Content)
      objResponse = responseCharge.data.Content;
      statusCode = responseCharge.data.StatusCode;
      console.log(statusCode);
    } else {
      customerId = selectors.customerCustomFormElement.value;
      const responseCard = await createCardImpl({ customerId, tokenId, deviceId }); // 1ra llamada a creacion de CARDS
      console.log(responseCard.data.Content)
      objResponse = responseCard.data.Content;
      statusCode = responseCard.data.StatusCode;
    }
    if (statusCode === 200) {
    var objeto = JSON.parse(objResponse);
		if(objeto.action_code === "REVIEW"){
			validationInit3DS({ email, statusCode, tokenId });
		}else{
			$("#response_card").text("ERROR AL REALIZAR LA OPERACIÓN");
		}
	   } else if (statusCode === 201) {
			$("#response_card").text("OPERACIÓN EXITOSA - SIN 3DS");
	      	Culqi3DS.reset();
       } else {
	      $("#response_card").text("OPERACIÓN FALLIDA - SIN 3DS");
	      	Culqi3DS.reset();
	   }
  } else {
    console.log(Culqi.error);
    alert(Culqi.error.user_message);
  }
};

const validationInit3DS = ({ statusCode, email, tokenId }) => {
    Culqi3DS.settings = {
      charge: {
        totalAmount: config.TOTAL_AMOUNT,
        returnUrl: config.URL_BASE,
      },
      card: {
        email: email,
      },
    };
    Culqi3DS.initAuthentication(tokenId);
};

async function createCustomer() {
  const dataCustomer = await createCustomerImpl({
    ...customerInfo,
  });
  var objeto = JSON.parse(dataCustomer.data.Content);
  $("#response_customer").text(objeto.id);
  console.log(objeto.id);
}

$("#loadCustomerExampleData").click(function () {
  selectors.customersNameElement.value = customerInfo.firstName;
  selectors.customersLastNameElement.value = customerInfo.lastName;
  selectors.customersEmailElement.value = customerInfo.email;
  selectors.customersAddressElement.value = customerInfo.address;
  selectors.customersPhoneElement.value = customerInfo.phone;
  selectors.customersAddressCityElement.value = customerInfo.address_c;
});

$("#crearCustomer").click(function () {
  customerInfo.firstName = selectors.customersNameElement.value;
  customerInfo.lastName = selectors.customersLastNameElement.value;
  customerInfo.email = selectors.customersEmailElement.value;
  customerInfo.address = selectors.customersAddressElement.value;
  customerInfo.phone = selectors.customersPhoneElement.value;
  customerInfo.address_c = selectors.customersAddressCityElement.value;
  createCustomer();
});

$("#crearCard").click(function () {
  Culqi.open();
});
