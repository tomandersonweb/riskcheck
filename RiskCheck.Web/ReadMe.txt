This is a .net core web app with a Web Api 2 controller and supporting .net core class projects.

It was developed using Visual Studio 2017

The api endpoint is located at /api/risk and has a POST method that accepts a single XML encoded risk:
<Risk>
<Name>Gordon Ramsey</Name>
<Occupation>Chef</Occupation>
<Address>
		<Address1>London</Address1>
		<Postcode>SW83JD</Postcode>
</Address>
<KeptPostcode>SW8 3JD</KeptPostcode>
</Risk>

A result is returned indicating if the risk was Accepted / Declined or Reffered 