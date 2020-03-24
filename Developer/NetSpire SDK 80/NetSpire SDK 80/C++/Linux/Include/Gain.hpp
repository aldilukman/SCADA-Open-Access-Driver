#ifndef _GAIN_HPP_
#define _GAIN_HPP_

#include "StandardIncludes.hpp"

/**
* Specifies amplifier gain level. The "level" field contains a numeric value in dBs.
*/
class Gain
{
public:
	/// Default constructor
	Gain();

	/// Constructor
	/// @param level Gain level. System limits levels to the range [-96.000, 0.000] dB. Values exceeding this range will be
	/// converted to the closest value within the range.
	Gain(double level);

	/// Set gain level.
	/// @param level Gain level. System limits levels to the range [-96.000, 0.000] dB. Values exceeding this range will be
	/// converted to the closest value within the range.
	void setLevel(double level);

	/// Get gain level
	/// @return  Gain level.
	double getLevel() const;

private:
	double level_;
	static const double maxGain_;
	static const double minGain_;
};

#endif // _GAIN_HPP_
