EloPayPal
==========

EloPayPal aims to be a fully Mono compatible .NET library that will greatly ease the use of PayPal's services. It helps check for usage errors as much as possible before making requests and provides a very well documented API that helps you discover it as you type. It also makes it easy - just one line of code - to switch between sandbox and production modes. On top of that, it has no dependencies other than the standard .NET framework. It targets .NET 4 or greater.

Current Status
-----
So far, only the Chained Payment, Simple Payment and PreApproval operations of the Adaptive Payments API and the GetVerifiedStatus request of the Adaptive Accounts API are implemented. There are no asynchronous methods to execute the requests. It also has classes to represent IPNs and to notify PayPal of it, making it easy to receive and treat them properly.
As a plus, it also has a **FeesCalculator** class that helps you calculate how much an item would have to cost for you to receive a certain amount, amongst other helpful calculations.

Usage
-----
The library's configuration is set through the **EloPayPal.Configuration.Set** method. This method receives a **EloPayPal.PayPalConfiguration** object, which represents both a registered PayPal application and some more settings. Let's create a sample PayPal Sandbox application:


```
using EloPayPal;

...

var conf = new SandboxConfiguration("apicalleremail@something.com", "paymentcanceledUrl", "paymentfinishedUrl", "currency (e.g. USD)", "IpnNotificationUrl", "UserId", "password", "signature");
Configuration.Set(conf);
```

We are not ready to use the library just yet. We need to plug a json serializer in EloPayPal to serialize and deserialize requests and responses. This is necessary so that EloPayPal won't force a specific json library when you already have one of your choice.
To do this, you need to implement the **EloPayPal.Serialization.IJsonSerializer** interface and register an instance of your implementing class within EloPayPal. Don't worry, this interface has only two methods. Here goes an example that uses ServiceStack.Text:

```
public class EloPayPalJsonSerializer : EloPayPal.Serialization.IJsonSerializer
{
	public string Serialize(object obj) {
		return JsonConvert.SerializeObject(obj);
	}
	
	public T Deserialize<T>(string jsonObj) {
		return JsonConvert.DeserializeObject<T>(jsonObj);
	}
}
```
Be aware of the following when you implement IJsonSerializer:
- You do not have to worry about how you handle dates. EloPayPal always converts dates to strings property to avoid Json serialization headaches;
- Your implementation of the method **Serialize(object obj)** has to treat a Dictionary<string, object> as if it were an object whose keys are properties and whose values are the values of these properties.

Once you have your implementation of the serializer ready, let's plug it in!

```
EloPayPal.Configuration.JsonSerializer = new EloPayPalJsonSerializer();
```

Great! Now we are ready to start making requests. In case you want to use a production application, take a look into **EloPayPal.ProductionConfiguration**!

Making requests
----
As a sample application, let's suppose you want to generate a payment of 10 dollars. It is very simple!

```
using EloPayPal;
using EloPayPal.Adaptive;

...

var payRequest = new SimplePayment(); // You can also inject a different PayPalConfiguration through the constructor

// Configure the receiver: the api caller themselves.
string apiCallerEmail = Configuration.Current.APICallerEmail;
var receiver = new PayPalReceiver(apiCallerEmail, 10, null); // null is required because this is a simple payment. In a chained payment you would have to specify if this is a primary receiver or not

payRequest.SetReceiver(receiver);

// Some custom settings and success url overriding
payRequest.PayKeyDuration = 30; // Our payments will expire in 30 minutes!
payRequest.PaymentSuccessUrl = "http://mywebsite.com/PaymentFinished?paymentid=7513;

// The execution, finally!
PayResponse response;
try
{
	if (payRequest.Execute(out response) != InstructionAck.Success)
	{
		// Some PayPal error, check the response object for more information
	}
	else
	{
		// This is good! Everything went right!
		// Proceed redirecting the user to the payment page
		RedirectUserTo(Configuration.Current.GetFinishPaymentUrl(response.paykey));
	}

}
catch (PayPalTimeoutException e)
{
	// Treat the exception. In this case this usually means telling the user to try again
	...
}
catch (UnknownServerResponseException e)
{
	// Log this and tell the developer of this library that he sucks! This is a very grave error and SHOULD NOT happen!
	...
}
```

Receiving IPNs
----
Still not done


Common errors
----
- Timeouts every time may imply a problem with the Mono.Security.dll assembly. Not using this library fixes this.
- Make sure you are accepting the proper certificates, take a look into ServicePointManager.ServerCertificateValidationCallback
