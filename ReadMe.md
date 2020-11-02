 
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

  



 