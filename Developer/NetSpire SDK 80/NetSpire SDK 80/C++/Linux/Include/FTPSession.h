#ifndef OA_FTPSESSION_H
#define OA_FTPSESSION_H

#define _FTPLIB_NO_COMPAT

#ifdef WIN32
#include "../../../3rdparty/ftplib/src/ftplib-3.1-1/winnt/ftplib.h"
#else
//#include "../../../3rdparty/ftplib/src/ftplib-3.1/linux/ftplib.h"
#endif

#define MAXSEQUENCENUMBER 30000

class OA_FTPSession
{
private:
	int my_SequenceNumber;
	char my_IPAddress[32];
	char my_UserName[32];
	char my_Password[32];
	
	netbuf *my_NetBuf;

	static int getNextSequenceNumber();
	
	char* getDirectoryName(const char* fileName);
	char* getFileTitle  (const char* fileName);

	void splitFile (const char *sourceFile, char * FirstFile, char * Balance);
	bool putSingleFile (char *sourceFile, char * targetFile);
	bool getSingleFile (char *sourceFile, char * targetFile);
	bool deleteSingleFile (char * targetFile);

	void Connect();

	// Defined private to ensure no one calls it
	OA_FTPSession (OA_FTPSession &Other);
public:
	OA_FTPSession (const char* ipAddress, const char* userName, const char* password);
	~OA_FTPSession();
	int getSequenceNumber() const;
	const char * getIPAddress() const;
	bool putFile (const char *sourceFile, const char * targetFile);
	bool getFile (const char *sourceFile, const char * targetFile);
	bool deleteFile (const char * targetFile);
	void changeDirectory(const char *dirName);
	char *listFiles(const char *dirName);
	int listOlderFiles(const char *srcDir, const char *tgtDir, const char *outFile);
};


#endif
