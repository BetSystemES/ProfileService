# ProfileService


ProfileService methods

| Method       | Description    |
| ------------- |-------------|
| AddProfileData()      | Function of add profile data |
| GetProfileDataById()      | Function of requesting profile data by user ID      |
| UpdateProfileData() | Function of updating profile data      |
| AddDiscount()      | Function of add discount for profile |
| GetDiscounts()      | Function of requesting profile discounts by user ID      |
| UpdateDiscount() | Function of updating discount      |


Business Models
```
 UserProfile {
  id = 1;
  first_name = 2;
  last_name = 3;
  phone = 4;
  email = 5;
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
Discount {
	id = 1;
	profile_id = 2;
	is_already_used = 3;
	DiscountType type = 4;
	amount = 5;
	discount_value = 6;	
}
```


Description of methons of gRPC Service
```
Function of add profile data:
	rpc AddProfileData (AddProfileDataRequest) 
		returns (AddProfileDataResponce);

Function of requesting profile data by user ID:
	rpc GetProfileDataById (GetProfileDataByIdRequest) 
		returns (GetProfileDataByIdResponce);

Function of updating profile data:
	rpc UpdateProfileData (UpdateProfileDataRequest) 
		returns (UpdateProfileDataResponce);

Function of add discount:
	rpc AddDiscount (AddDiscountRequest) 
		returns (AddDiscountResponce);

Function of requesting profile discounts by user ID:
	rpc GetDiscounts (GetDiscountsRequest) 
		returns (GetDiscountsResponce);
  
Function of updating discounts:
	rpc UpdateDiscount (UpdateDiscountRequest) 
		returns (UpdateDiscountResponce);
  ```
  
  ```
  Example of UserProfile proto-entity
  
{
	"user_profile" : 
	{
		"id" : "8f902da9-e152-4864-8b5d-3c36a3c6f496",
		"first_name" : "Pavel",
		"last_name" : "K",
		"phone" : "444333222",
		"email" : "PavelK@google.com"
	}
}
```

```
Example of ProfileByIdRequest  proto-entity

{
	"profile_by_id_request" : 
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
		"profile_id" : "8f902da9-e152-4864-8b5d-3c36a3c6f496",
		"is_already_used" : true,
		"type" : "DISCOUNT_TYPE_AMOUNT",
		"amount" : 50.0,
		"discount_value" : 30.0
	}
}
```

