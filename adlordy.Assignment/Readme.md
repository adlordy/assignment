Diff Assingment
===============
## Running

* Compile **adlordy.Assignment** using  Visual Studio 2015
* Start **OWIN** self-hosted Web API service by running console
* "Listening at http://localhost:8081" message will appear.
* Use **REST** debugging tools such as [Postman](https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop?hl=en) to
submit following requests
* Send **POST** following JSON `{data:"dGVzdA=="}` to http://localhost:8081/v1/diff/left 
with `Content-Type: application/json` header
* Send **POST**  following JSON `{data:"dGVzcw=="}` to http://localhost:8081/v1/diff/right
with `Content-Type: application/json` header
* Send **GET** to http://localhost:8081/v1/diff with `Accept: application/json` header
* You should get following a result:
```json
{
    "diffResultType": "ContentDoNotMatch",
    "diffs": [
        {
            "offset": 3,
            "length": 1
        }
    ]
}
```

## Testing

Use Visual Studio 2015 to run the tests located in **adlordy.Assignment.Tests** project. 