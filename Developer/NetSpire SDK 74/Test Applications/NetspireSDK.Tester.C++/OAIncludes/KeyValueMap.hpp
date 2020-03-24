#ifndef _KEYVALUEMAP_HPP_
#define _KEYVALUEMAP_HPP_

#include "StandardIncludes.hpp"

/*
* A data structure to map from keys to values
*/
class KeyValueMap
{
public:
	KeyValueMap ();
	~KeyValueMap ();

	/*
	* Adds the specified key/value pair to this collection.
	* @param key The name of the key
	* @param value The value associated with this key
	*/
	void set (const std::string& key, const std::string& value);

	/*
	* Returns true if the specified key is present
	* @param key The name of the key
	* @return True if the key is present, false otherwise
	*/
	bool has_key (const std::string& key) const;

	/*
	* Returns true if this data structure has no keys
	* @return True if there are no keys
	*/
	bool empty () const;

	/*
	* Erases all contents of this data structure
	*/
	void clear ();

	/*
	* Returns the number of elements in this collection.
	* @return The current count of elements
	*/
	size_t size () const;

private:
	std::map<std::string,std::string> content;
};

#endif // _KEYVALUEMAP_HPP_
