# PaymentAPI

This application provides the basic functionality of a payment gateway through API calls that a merchant can utilise to process payments with banks.

## Features include:
1. .Net MVC/WebAPI 4.7.2
2. Entity Framework 6
3. Client Side
4. Bank simulation

## Solution include:
This Project consist of 3 Parts which is the **ClientSide**, **ApiGateway** and **BankSide**.All of these projects are build on .NET framework 4.7.2

### ClientSide:
The ClientSide is where you can see your previous transaction and can make a payment right now.                                              
**Note:** you need to login to get access to the dashboard and an existing credential will be provide to you

### ApiGateway
The ApiGateway allows to get transaction details from the client to proceed the payment to the bank. It has a Local database on project it stored the transaction details. There two keys which are used to identify the transactions
 1. 

