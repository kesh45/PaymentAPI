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
The ApiGateway allows to get transaction details from the client to proceed the payment to the bank. It has a Local database on project it stored the transaction details. There two keys which are used to identify the transactions.

 1. Personal_Token (This is generated on the project which allow to identify the User).
 2. Payment_Token (This is generated on the project When a new payment is processed for the user).
 
### BankSide
The BankSide is where your transaction is being processed and you can check your balance account by providing your Card Number and CVV number.

If your transaction is approved ,It will return two values to the ApiGateway which are **approval_response** (true or False) and **Payment_Token** (the transaction that has been generated from the ApiGateway).

## API Request
#### ApiGateways ####
The following link is used to post a new transaction

