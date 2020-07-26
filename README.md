# json-for-splunk

This demonstrates two methods of generating JSON-ish key-value formatted output suitable for indexing by Splunk.

I say 'JSON-ish' because Splunk doesn't require a well-formed JSON file, just well-formed JSON on an individual line.

For example, Splunk will index this just fine:

## Sort of JSON

```
{"key1":"value1","key2":"value2"}
{"key1":"value1","key2":"value2"}
```

Note that each line is well-formed JSON.  But, taken as a whole, it is not.  If you really wanted it to be well-formed JSON, you'd need to do something like this:

## Actual JSON

```
{
	"records": [
		{
			"key1": "value1",
			"key2": "value2"
		},
		{
			"key1": "value1",
			"key2": "value2"
		}
	]
}
```

## Summary

So remember, output well-formed JSON on each line, then Splunk will index the key-value pairs so that they can be incorporated into Splunk queries.
