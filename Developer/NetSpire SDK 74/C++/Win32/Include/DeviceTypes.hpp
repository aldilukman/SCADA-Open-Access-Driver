#ifndef _DEVICE_TYPES_HPP_
#define _DEVICE_TYPES_HPP_

#include "StandardIncludes.hpp"
#include "Device.hpp"

// An instance of a TCX (Train Communications Exchange)
class DeviceTCX : public Device
{
public:
	DeviceTCX (int elemNo, const std::string &deviceId, int nsType, int dstNo, int portNo, const std::string &deviceModelName);
	~DeviceTCX ();
private:
};

// An instance of a TGU (Train Gateway Unit)
class DeviceTGU : public Device
{
public:
	DeviceTGU (int elemNo, const std::string &deviceId, int nsType, int dstNo, int portNo, const std::string &deviceModelName);
	~DeviceTGU ();
private:
};

// An instance of a CI (Crew Intercom)
class DeviceCI : public Device
{
public:
	DeviceCI (int elemNo, const std::string &deviceId, int nsType, int dstNo, int portNo, const std::string &deviceModelName);
	~DeviceCI ();

private:
	bool status_TrainRadioOK;       // DIO - input
	bool status_PARelayOut;         // DIO - input
	bool status_CallInProgress;     // DIO - input
};

// An instance of a Crew Speaker (in each crew cabin).
class DeviceCrewSpeaker : public Device
{
public:
	DeviceCrewSpeaker (int elemNo, const std::string &deviceId, int nsType, int dstNo, int portNo, const std::string &deviceModelName);
	~DeviceCrewSpeaker ();
	void setGain (int dB) throw (std::out_of_range);
	int getGain () const;
	void setDVAPAMonitoring (bool enable);
	bool isMonitoring () const;
private:
	static const int minDb = -78;
	static const int maxDb =  18;
	static const int defaultSpeakerGain = 0;
	int gain; // dB
	bool monitoringDVAPA;
};

// An instance of a PEI (Personal Emergency Intercom)
class DevicePEI : public Device
{
public:
	DevicePEI (int elemNo, const std::string &deviceId, int nsType, int dstNo, int portNo, const std::string &deviceModelName);
	~DevicePEI ();
	void setEDRStatus(bool enable);
	bool getEDRStatus() const;
private:
	bool EDRStatus;     // Emergency Door Release - valid for disabled PEIs (Thales Project) only
};

// An instance of an IDI (Internal Destination Indicator)
class DeviceIDI : public Device
{
public:
	DeviceIDI (int elemNo, const std::string &deviceId, int nsType, int dstNo, int portNo, const std::string &deviceModelName);
	~DeviceIDI ();
	void display (const std::string& s);
	std::string getLastMessage () const;
private:
	std::string lastMessage;
};

// An instance of an EDI (External Destination Indicator)
class DeviceEDI : public Device
{
public:
	DeviceEDI (int elemNo, const std::string &deviceId, int nsType, int dstNo, int portNo, const std::string &deviceModelName);
	~DeviceEDI ();
	void display (const std::string &s);
	std::string getLastMessage () const;
private:
	std::string lastMessage;
};

// An instance of TR (Train Radio)
class DeviceTR : public Device
{
public:
	DeviceTR (int elemNo, const std::string &deviceId, int nsType, int dstNo, int portNo, const std::string &deviceModelName);
	~DeviceTR ();
private:
};

// An instance of IP Microphone
class DeviceIPS : public Device
{
public:
	DeviceIPS (int elemNo, const std::string &deviceId, int nsType, int dstNo, int portNo, const std::string &deviceModelName);
	~DeviceIPS ();
private:
};

// An instance of a VCU (Video Controller Unit)
class DeviceVCU : public Device
{
public:
	DeviceVCU (int elemNo, const std::string &deviceId, int nsType, int dstNo, int portNo, const std::string &deviceModelName);
	~DeviceVCU ();
private:
};

#endif // _DEVICE_TYPES_HPP_
