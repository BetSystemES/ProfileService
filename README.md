# ProfileService

Business Models

 PersonalData {
   id 
   name 
   surname 
   phone 
   email 
}
 Transaction {
	 id 
	 amount 
	 datatime 
}

enum DiscountType {
	AMOUNT = 0;
	DISCOUNT = 1;
}

 Discount{
	 id 
	 type 
	 amount 
	 discount 	
}



Description of Methons of GRPC Service


Function of requesting personal data by user ID
    rpc GetPersonalData (PersonalRequest) returns (PersonalDataResponce);

Function of requesting personal account transactions by by user ID
  rpc GetTransactions (PersonalRequest) returns (TransactionsResponce);


Function of requesting personal discounts  by user ID
  rpc GetDiscounts (PersonalRequest) returns (DiscountsResponce);
