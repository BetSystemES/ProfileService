# ProfileService

Business Models

 PersonalData {
   id 
   name 
   surname 
   phone 
   email 
}

enum DiscountType {
	AMOUNT = 0;
	DISCOUNT = 1;
}

 Discount{
	 id
	 isalreadyused
	 type 
	 amount 
	 discount 	
}

Description of Methons of GRPC Service


Function of requesting personal data by user ID
    rpc GetPersonalDataById (ProfileByIdRequest) returns (PersonalDataResponce);

Function of updating personal data
  rpc UpdatePersonalData (PersonalDataRequest) returns (BasicVoidResponce);

Function of requesting personal discounts by user ID
  rpc GetDiscounts (ProfileByIdRequest) returns (DiscountsResponce);
  
Function of updating discounts
  rpc UpdateDiscount (Discount) returns (BasicVoidResponce);
  
  ```
  Example of PersonalProfile proto-entity
  
  {
	"PersonalProfile" : 
	{
   "id" : "8f902da9-e152-4864-8b5d-3c36a3c6f496",
   "name" : "Pavel",
   "surname" : "K",
   "phone" : "444333222",
   "email" : "PavelK@google.com"
	}
}
```
  
  
