 
 - MediatR: to process commands

 - For validations, there are custome exception which is handled via exception middleware which returns BadRequest with ValidationErrors

 - Endpoint to create a contact with address
     - Command: CreateContactCommand + CreateContactCommandHandler
	 - Events: ContactCreated
	 - Events: AddressUpdated
	 
 - Endpoint to Link a Company with ContactCreated
	 - Command: AddCompanyCommand + AddCompanyCommandhandler
	 - Events: CompanyAdded

 - Endpoint to remove a Company from Contact
	 - Command: DeleteCompanyCommand + DeleteCompanyCommandHandler
	 - Events: CompanyDeleted

  


![alt text](https://github.com/hanzalahsuriya/PS_ES_Test/blob/master/1.JPG)

![alt text](https://github.com/hanzalahsuriya/PS_ES_Test/blob/master/2.JPG)

![alt text](https://github.com/hanzalahsuriya/PS_ES_Test/blob/master/3.JPG)

![alt text](https://github.com/hanzalahsuriya/PS_ES_Test/blob/master/4.JPG)

![alt text](https://github.com/hanzalahsuriya/PS_ES_Test/blob/master/5.JPG)

![alt text](https://github.com/hanzalahsuriya/PS_ES_Test/blob/master/6.JPG)

![alt text](https://github.com/hanzalahsuriya/PS_ES_Test/blob/master/7.JPG)

![alt text](https://github.com/hanzalahsuriya/PS_ES_Test/blob/master/8.JPG)

![alt text](https://github.com/hanzalahsuriya/PS_ES_Test/blob/master/9.JPG)

![alt text](https://github.com/hanzalahsuriya/PS_ES_Test/blob/master/10.JPG)


