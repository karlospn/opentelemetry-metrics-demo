#!/bin/bash

#########################
##### Set API URI #######
#########################
api_uri=http://localhost:5001

#########################
##### Add Categories ####
#########################

curl -k -X 'POST' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"name": "Educational"}'
echo
curl -k -X 'POST' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"name": "Drama"}'
echo
curl -k -X 'POST' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"name": "Fantasy"}'
echo
curl -k -X 'POST' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"name": "Mistery"}'
echo
curl -k -X 'POST' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"name": "SciFi"}'
echo
curl -k -X 'POST' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"name": "Western"}'
echo
curl -k -X 'POST' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"name": "Contemporary"}'
echo
curl -k -X 'POST' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"name": "Dystopian"}'
echo
sleep 5

#########################
### Update Categories ###
#########################

curl -k -X 'PUT' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"id": 2, "name": "History"}'
echo
curl -k -X 'PUT' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"id": 4, "name": "Travel"}'
echo
curl -k -X 'PUT' "${api_uri}/api/Categories" -H 'accept: */*' -H 'Content-Type: application/json' -d '{"id": 6, "name": "Memoir"}'
echo
sleep 5

#########################
### Delete Categories ###
#########################

curl -k -X 'DELETE' "${api_uri}/api/Categories/6" -H 'accept: */*'
echo
curl -k -X 'DELETE' "${api_uri}/api/Categories/3" -H 'accept: */*'
echo
sleep 5

#########################
###### Add Books  #######
#########################

curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 1,
  "name": "Parallel Programming and Concurrency with C# 10 and .NET 6",
  "author": "Alvin Ashcraft",
  "description": "Leverage the latest parallel and concurrency features in .NET 6 when building your next application and explore the benefits and challenges of asynchrony, parallelism, and concurrency in .NET via practical examples.",
  "value": 37.99,
  "publishDate": "2022-08-30T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 1,
  "name": "Mapping Data Flows in Azure Data Factory",
  "author": "Mark Kromer",
  "description": "Build scalable ETL data pipelines in the cloud using Azure Data Factory Mapping Data Flows.",
  "value": 49.99,
  "publishDate": "2022-08-26T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 1,
  "name": "Programming C# 10",
  "author": "Ian Griffiths",
  "description": "C# is undeniably one of the most versatile programming languages available to engineers today. With this comprehensive guide, you will learn just how powerful the combination of C# and .NET can be.",
  "value": 56.61,
  "publishDate": "2022-09-13T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 1,
  "name": "Design Patterns in .NET 6",
  "author": "Dmitri Nesteruk",
  "description": "Implement design patterns in .NET 6 using the latest versions of the C# and F# languages.",
  "value": 56.60,
  "publishDate": "2022-08-30T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 1,
  "name": "Practical Database Auditing for Microsoft SQL Server and Azure SQL",
  "author": "Josephine Bush",
  "description": "Know how to track changes and key events in your SQL Server databases in support of application troubleshooting, regulatory compliance, and governance.",
  "value": 49.99,
  "publishDate": "2022-09-20T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 1,
  "name": "Software Architecture with C# 10 and .NET 6",
  "author": "Gabriel Baptista",
  "description": "Design scalable and high-performance enterprise applications using the latest features of C# 10 and .NET 6.",
  "value": 49.39,
  "publishDate": "2022-03-15T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 1,
  "name": "Patterns of Enterprise Application Architecture",
  "author": "Martin Fowler",
  "description": "Developers of enterprise applications (e.g reservation systems, supply chain programs, financial systems, etc.) face a unique set of challenges, different than those faced by their desktop system and embedded system peers.",
  "value": 60.99,
  "publishDate": "2002-11-05T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 2,
  "name": "History of the World Map by Map",
  "author": "DK",
  "description": "Explore the history of the world in unprecedented detail with this ultimate guide to history throughout the ages.",
  "value": 35.40,
  "publishDate": "2018-10-23T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 2,
  "name": "Pagan Christianity: Exploring the Roots of Our Church Practices",
  "author": "Frank Viola",
  "description": "Have you ever wondered why we Christians do what we do for church every Sunday morning? Why do we dress up for church?.",
  "value": 15.39,
  "publishDate": "2012-02-01T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 2,
  "name": "National Geographic Atlas of the National Parks",
  "author": "Jon Waterman",
  "description": "The first book of its kind, this stunning atlas showcases America park system from coast to coast, richly illustrated with an inspiring and informative collection of maps, graphics, and photographs.",
  "value": 44.49,
  "publishDate": "2019-11-19T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 4,
  "name": "National Geographic Road Atlas 2022",
  "author": "National Geographic",
  "description": "National Geographic Road Atlas: Adventure Edition, is the ideal companion for the next time you hit the road.",
  "value": 22.46,
  "publishDate": "2019-01-10T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 4,
  "name": "Rick Steves Paris",
  "author": "Rick Steves",
  "description": "Now more than ever, you can count on Rick Steves to tell you what you really need to know when traveling through Paris.",
  "value": 19.79,
  "publishDate": "2022-09-20T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 4,
  "name": "Rick Steves London",
  "author": "Rick Steves",
  "description": "Now more than ever, you can count on Rick Steves to tell you what you really need to know when traveling through London.",
  "value": 19.79,
  "publishDate": "2022-09-06T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 7,
  "name": "West with Giraffes: A Novel",
  "author": "Linda Rutledge",
  "description": "An emotional, rousing novel inspired by the incredible true story of two giraffes who made headlines and won the hearts of Depression-era America.",
  "value": 10.99,
  "publishDate": "2021-02-01T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 8,
  "name": "The Big Dark Sky",
  "author": "Dean Koontz",
  "description": "A group of strangers bound by terrifying synchronicity becomes humankinds hope of survival in an exhilarating, twist-filled novel by Dean Koontz.",
  "value": 12.99,
  "publishDate": "2022-07-19T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 8,
  "name": "Ready Player Two: A Nove",
  "author": "Ernes Cline",
  "description": "The thrilling sequel to the beloved worldwide best seller Ready Player One, the near-future adventure that inspired the blockbuster Steven Spielberg film.",
  "value": 13.99,
  "publishDate": "2020-11-24T00:00:00.000Z"
}'
echo
curl -k -X 'POST' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "categoryId": 8,
  "name": "Morning Star: Book III of the Red Rising Trilogy",
  "author": "Pierce Brown",
  "description": "Darrow would have lived in peace, but his enemies brought him war. The Gold overlords demanded his obedience, hanged his wife, and enslaved his people. But Darrow is determined to fight back.",
  "value": 14.55,
  "publishDate": "2016-02-16T00:00:00.000Z"
}'
echo
sleep 5

#########################
#### Update Books  ######
#########################

curl -k -X 'PUT' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "id": 15,
  "categoryId": 8,
  "name": "The Big Dark Sky",
  "author": "Dean Koontz",
  "description": "A group of strangers bound by terrifying synchronicity becomes humankinds hope of survival in an exhilarating, twist-filled novel by Dean Koontz.",
  "value": 15.99,
  "publishDate": "2022-07-19T00:00:00.000Z"
}'
echo
curl -k -X 'PUT' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "id": 17
  "categoryId": 8,
  "name": "Morning Star: Book III of the Red Rising Trilogy",
  "author": "Pierce Brown",
  "description": "Darrow would have lived in peace, but his enemies brought him war. The Gold overlords demanded his obedience, hanged his wife, and enslaved his people. But Darrow is determined to fight back.",
  "value": 10.55,
  "publishDate": "2016-02-16T00:00:00.000Z"
}'
echo
curl -k -X 'PUT' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "id": 17
  "categoryId": 8,
  "name": "Morning Star: Book III of the Red Rising Trilogy",
  "author": "Pierce Brown",
  "description": "Darrow would have lived in peace, but his enemies brought him war. The Gold overlords demanded his obedience, hanged his wife, and enslaved his people. But Darrow is determined to fight back.",
  "value": 9.49,
  "publishDate": "2016-02-16T00:00:00.000Z"
}'
echo
curl -k -X 'PUT' "${api_uri}/api/Books" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "id": 16  
  "categoryId": 8,
  "name": "Ready Player Two: A Nove",
  "author": "Ernes Cline",
  "description": "The thrilling sequel to the beloved worldwide best seller Ready Player One, the near-future adventure that inspired the blockbuster Steven Spielberg film.",
  "value": 9.99,
  "publishDate": "2020-11-24T00:00:00.000Z"
}'
echo
sleep 5

#########################
#### Delete Books  ######
#########################

curl -k -X 'DELETE' "${api_uri}/api/Books/16" -H 'accept: */*'
echo
curl -k -X 'DELETE' "${api_uri}/api/Books/17" -H 'accept: */*'
echo
sleep 5

#########################
#### Add Inventory ######
#########################

curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 1,
  "amount": 50
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 2,
  "amount": 25
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 3,
  "amount": 30
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 4,
  "amount": 20
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 5,
  "amount": 35
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 6,
  "amount": 50
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 7,
  "amount": 20
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 8,
  "amount": 50
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 9,
  "amount": 25
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 10,
  "amount": 55
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 11,
  "amount": 60
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 12,
  "amount": 50
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 13,
  "amount": 60
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 14,
  "amount": 25
}'
echo
curl -k -X 'POST' "${api_uri}/api/Inventories" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "bookId": 15,
  "amount": 75
}'
echo
sleep 5

#########################
# Delete fake Inventory #
#########################
 
curl -k -X 'DELETE' "${api_uri}/api/Inventories/33" -H 'accept: */*'
echo
sleep 5

#########################
###### Add Orders  ######
#########################

curl -k -X 'POST' "${api_uri}/api/Orders" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "customerName": "John Doe",
  "address": "7 PENN NEW YORK NY 10001-0011",
  "telephone": "607-261-0843",
  "city": "New York",
  "books": [
    8,10
  ]
}'
echo
curl -k -X 'POST' "${api_uri}/api/Orders" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "customerName": "Mary Jones",
  "address": "208 W 30TH NEW YORK NY 10001-1017",
  "telephone": "607-261-0843",
  "city": "New York",
  "books": [
    1,2,6
  ]
}'
echo
curl -k -X 'POST' "${api_uri}/api/Orders" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "customerName": "Joe Munson",
  "address": "601 W 26TH NEW YORK NY 10001-1115",
  "telephone": "315-277-6032",
  "city": "New York",
  "books": [
    7
  ]
}'
echo
curl -k -X 'POST' "${api_uri}/api/Orders" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "customerName": "Jeff Goals",
  "address": "651 W HANCOCK DETROIT MI 48201-1147",
  "telephone": "313-201-8703",
  "city": "Detroit",
  "books": [
    3,11
  ]
}'
echo
curl -k -X 'POST' "${api_uri}/api/Orders" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "customerName": "Dan Mire",
  "address": "702 W CANFIELD DETROIT MI 48201-1135",
  "telephone": "313-938-3526",
  "city": "Detroit",
  "books": [
    7
  ]
}'
echo
curl -k -X 'POST' "${api_uri}/api/Orders" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "customerName": "James Roberts",
  "address": "800 POLO CLUB AUSTIN TX 78737-2614",
  "telephone": "512-217-1539",
  "city": "Austin",
  "books": [
    3,4,6,10,12
  ]
}'
echo
curl -k -X 'POST' "${api_uri}/api/Orders" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "customerName": "John Jones",
  "address": "300 WINECUP AUSTIN TX 78737-4562",
  "telephone": "512-555-0122",
  "city": "Austin",
  "books": [
    8
  ]
}'
echo
curl -k -X 'POST' "${api_uri}/api/Orders" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "customerName": "Audrey Fills",
  "address": "400 WHIRLAWAY AUSTIN TX 78737-2631",
  "telephone": "512-555-0139",
  "city": "Austin",
  "books": [
    1,14
  ]
}'
echo
curl -k -X 'POST' "${api_uri}/api/Orders" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "customerName": "Julia Boris",
  "address": "900 COURTLAND ATLANTA TX 75551-1531",
  "telephone": "404-555-0177",
  "city": "Atlanta",
  "books": [
    10
  ]
}'
echo
curl -k -X 'POST' "${api_uri}/api/Orders" -H 'accept: */*' -H 'Content-Type: application/json' \
-d '{
  "customerName": "Lori Loomis",
  "address": "1500 WESTLAKE SEATTLE WA 98109-3036",
  "telephone": "202-555-0158",
  "city": "Seattle",
  "books": [
    11,12,13
  ]
}'
echo
sleep 5

#########################
##### Cancel Orders #####
#########################

curl -k -X 'DELETE' "${api_uri}/api/Orders/3" -H 'accept: */*'
echo
curl -k -X 'DELETE' "${api_uri}/api/Orders/4" -H 'accept: */*'
echo
curl -k -X 'DELETE' "${api_uri}/api/Orders/9" -H 'accept: */*'
echo
