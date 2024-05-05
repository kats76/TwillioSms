using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WorkerService1;
using WorkerService1.Models;

namespace sms_test;

public static class SendSMS
{
    public static (string, string) SendMessageTwillo(string mensaje, PacienteDTO paciente)
    {
        try
        {
            TwilioClient.Init(Global.ACCOUNT_SID, Global.AUTH_TOCKEN);

            var message = MessageResource.Create(
                body: mensaje,
                from: new Twilio.Types.PhoneNumber(Global.PHONE_NUMBER),
                to: new Twilio.Types.PhoneNumber(paciente.celular)
            );

            string estadoEnvio = message.Status.ToString();

            return (estadoEnvio, message.Sid);
        }
        catch (Exception ex) 
        {
            return (ex.Message, string.Empty);
        }
       
    }
}

