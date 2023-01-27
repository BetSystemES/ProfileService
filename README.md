# ProfileService

Business Models
```
 PersonalData {
   id 
   name 
   surname 
   phone 
   email 
}
```
```
enum DiscountType {
	DISCOUNT_TYPE_UNSPECIFIED = 0;
	DISCOUNT_TYPE_AMOUNT = 1;
	DISCOUNT_TYPE_DISCOUNT = 2;
}
```
```
 Discount{
	 id
	 personalid
	 isalreadyused
	 type 
	 amount 
	 discountvalue 	
}
```


Description of Methons of GRPC Service
```
Function of add personal data
	rpc AddPersonalData (AddPersonalDataRequest) 
		returns (AddPersonalDataResponce);

Function of requesting personal data by user ID
	rpc GetPersonalDataById (GetPersonalDataByIdRequest) 
		returns (GetPersonalDataByIdResponce);

Function of updating personal data
	rpc UpdatePersonalData (UpdatePersonalDataRequest) 
		returns (UpdatePersonalDataResponce);

Function of add discount
	rpc AddDiscount (AddDiscountRequest) 
		returns (AddDiscountResponce);

Function of requesting personal discounts by user ID
	rpc GetDiscounts (GetDiscountsRequest) 
		returns (GetDiscountsResponce);
  
Function of updating discounts
	rpc UpdateDiscount (UpdateDiscountRequest) 
		returns (UpdateDiscountResponce);
  ```
  
  ```
  Example of PersonalProfile proto-entity
  
  {
	"personalprofile" : 
	{
   "id" : "8f902da9-e152-4864-8b5d-3c36a3c6f496",
   "name" : "Pavel",
   "surname" : "K",
   "phone" : "444333222",
   "email" : "PavelK@google.com"
	}
}
```

```
Example of ProfileByIdRequest  proto-entity
{
	"profilebyidrequest" : 
	{
   "id" : "8f902da9-e152-4864-8b5d-3c36a3c6f496"
	}
}
```

```
Example of Discount proto-entity
{
	"discount" : 
	{
	 "id" : "34c92d2c-1f47-4a04-bffa-71101718b56d",
   "personalid" : "8f902da9-e152-4864-8b5d-3c36a3c6f496",
   "isalreadyused" : true,
   "type" : "DISCOUNT_TYPE_AMOUNT",
   "amount" : 50.0,
   "discountvalue" : 30.0
	}
}
```

