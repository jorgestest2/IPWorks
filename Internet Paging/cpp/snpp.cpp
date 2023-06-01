/*
 * IPWorks 2022 C++ Edition - Sample Project
 *
 * This sample project demonstrates the usage of one or more classes in a 
 * simple, straightforward way.  This is not intended to be a complete 
 * application. Error handling and other checks are simplified for clarity.
 *
 * Copyright (c) 2023 /n software inc. www.nsoftware.com
 */


#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "../../include/ipworks.h"

#define LINE_LEN 80

class MySNPP: public SNPP
{
public:
	bool waiting;

	virtual int FirePITrail(SNPPPITrailEventParams *e)
	{
		waiting = false;
		printf(  "%d  %s\n", e->Direction, e->Message );
		return 0;
	}
	virtual int FireError(SNPPErrorEventParams *e)
	{
		printf("Error %i: %s", e->ErrorCode, e->Description);
		exit(0);
	}
	virtual int FireSSLServerAuthentication(SNPPSSLServerAuthenticationEventParams *e)
	{
		if (e->Accept) return 0;
		printf("Server provided the following certificate:\nIssuer: %s\nSubject: %s\n",
			e->CertIssuer, e->CertSubject);
		printf("The following problems have been determined for this certificate: %s\n", e->Status);
		printf("Would you like to continue anyways? [y/n] ");
		if (getchar() == 'y') e->Accept = true;
		else exit(0);
		return 0;
	}
};


int main (int argc, char * argv[])
{
	if (argc != 4) {

		fprintf(stderr, "usage: snpp server pagerid message\n\n");
		fprintf(stderr, "  server   the name or address of the SNPP server\n");
		fprintf(stderr, "  pagerid  the identifying number of the pager to send a Message to\n");
		fprintf(stderr, "  message  the message content\n");
		fprintf(stderr, "\nExample: snpp SnppServer pagerId \"test message\"\n\n");
		printf("Press enter to continue.");
		getchar();

	}
	else{
		MySNPP snpp;
		
		snpp.SetServerName(argv[1]);

		snpp.SetPagerId(argv[2]);

		snpp.SetMessage(argv[3]);

		snpp.Connect();
		if (!snpp.GetConnected())
		{
			printf("Cannot connect to server. Press enter to continue...\n");
			getchar();
			exit(0);
		}
		snpp.Send();

		printf("Page sent. Press enter to continue...");
		getchar();
		exit(1);
		return 0;
	}	
}









