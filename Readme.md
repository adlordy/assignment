### Required features
    
- Provide 2 http endpoints (`<host>/v1/diff/<ID>/left` and `<host>/v1/diff/<ID>/right`) that accept JSON containing base64 encoded binary data on both endpoints.
- The provided data needs to be diff-ed and the results shall be available on a third end point (`<host>/v1/diff/<ID>`). The results shall provide the following info in JSON format: 
  - If equal return that
  - If not of equal size just return that
  - If of same size provide insight in where the diff are, actual diffs are not needed.
    - So mainly offsets + length in the data

Make assumptions in the implementation explicit, choices are good but need to be communicated.

Upload code to a public repository using GIT.

  
### Required technology
- C#
- Functionality shall be under integration tests
- Internal logic shall be under unit tests 
- Documentation in code
- Short readme on usage
 
### Sample input/output

<table>
<thead>
<tr>
    <td>End-point</td>
    <td>Request</td>
    <td>Response</td>
</tr>
</thead>
<tbody>
<tr>
<td>1</td>
<td>
GET /v1/diff/1
</td>
<td>404 Not Found</td>
</tr>
<tr>
<tr>
<td>2</td>
<td>
PUT /v1/diff/1/left
<pre>
{
  "data": "AAAAAA=="
}
</pre>
</td>
<td>201 Created</td>
</tr>
<tr>
<tr>
<td>3</td>
<td>
GET /v1/diff/1
</td>
<td>404 Not Found</td>
</tr>
<tr>
<tr>
<td>4</td>
<td>
PUT /v1/diff/1/right<pre>
{
  "data": "AAAAAA=="
}
</pre>
</td>
<td>201 Created</td>
</tr>
<tr>
<td>5</td>
<td>GET /v1/diff/1</td>
<td>200 OK
<pre>
{
  "diffResultType": "Equals"
}
</pre>
</td>
</tr>
<tr>
<td>6</td>
<td>PUT /v1/diff/1/right
<pre>
{
  "data": "AQABAQ=="
}
</pre>
<td>201 Created</td>
</tr>
<tr>
<td>7</td>
<td>GET /v1/diff/1</td>
<td>
200 OK
<pre>
{
  "diffResultType": "ContentDoNotMatch",
  "diffs": [
    {
      "offset": 0,
      "length": 1
    },
    {
      "offset": 2,
      "length": 2
    }
  ]
}
</pre>
</td>
<tr>
<td>8</td>
<td>PUT /v1/diff/1/left
<pre>
{
   "data": "AAA="
}
</pre>
<td>
201 Created
</td>
</tr>
<tr>
<td>9</td>
<td>GET /v1/diff/1</td>
<td>
200 OK
<pre>
{
  "diffResultType": "SizeDoNotMatch"
}
</pre>
</tr>
<tr>
<td>10</td>
<td>PUT /v1/diff/1/left
<pre>
{
   "data": null
}
</pre>
<td>
400 Bad Request
</td>
</tr>
</tbody>
</table>
