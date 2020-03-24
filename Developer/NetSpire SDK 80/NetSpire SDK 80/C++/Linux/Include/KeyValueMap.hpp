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
	* Returns list of all keys in the collection
	* @return list of keys
	*/
	std::vector<std::string> get_all_keys() const;

	/*
	* Returns true if the specified key is present
	* @param key The name of the key
	* @return True if the key is present, false otherwise
	*/
	bool has_key (const std::string& key) const;

	/*
	* Returns value for the requested key
	* @param key The name of the key
	* @return value stored for the key
	*/
	std::string get_value(const std::string& key) const;

	/*
	* Removes key/value from collection
	* @param key The name of the key
	* @return true if remove was success else false
	*/
	bool erase(const std::string& key);

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
