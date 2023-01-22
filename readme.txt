- to run elastic search and kibana
docker-compose up
- to learn status
http://localhost:9200
- get total count
GET products/_count
 - to get one document with id
GET products/_doc/1
 - to get documents with ids
GET products/_search
 {
  "query": {
    "ids": {
      "values": [1,2,3]
    }
  }
 }
 - to get all documents
 GET products/_search or  
 GET products/_search
{
  "query": {
    "match_all": {}
  }
}
- Full text queries ( like SQL Like Command )
GET products/_search
{
  "query": {
    "match": {
      "name": "Iphone"  // don't matter iphone or IPHone
    }
  }
}
- Prefix Query
GET products/_search
{
  "query": {
    "prefix": {
      "name": "iph"
    }
  }
}
- match searching ( implicity or )
GET products/_search
{
  "query": {
    "match": {
      "name": "IPhone 14" // IPhone or 14
    }
  }
}
- match searching ( And keyword)
GET products/_search
{
  "query": {
    "match": {
      "name": {
        "query": "IPhone 14",
        "operator": "and"
      }
      
    }
  }
}
- searching across multiple fields
GET products/_search
{
  "query": {
    "multi_match": {
      "query": "mobile phones",
      "fields": ["name","category"]
    }
  }
}
- boosting results :  we want to give  a higher priority to certain fields
GET products/_search
{
  "query": {
    "multi_match": {
      "query": "mobile phones",
      "fields": ["name^3","category"]
    }
  }
}
- search on a phrase
GET products/_search
{
"query": {
  "match_phrase": {
    "name": "Nokia mobile phone 1" // exactly search
  }
}
}

-highlighting the result
GET products/_search
{
"query": {
  "match_phrase": {
    "name": "Nokia mobile"
  }
}
, "highlight": {
  "fields": {
    "name": {}
  }
}
}
- Phrases with missing words
GET products/_search
{
"query": {
  "match_phrase": {
    "name":{
      "query": "Nokia phone 1",
      "slop": 1
    }
  }
}
}
- Matching phrases with a prefix
GET products/_search
{
"query": {
  "match_phrase_prefix": {
    "name": "Nokia mob"
  }
}
}
- Fuzzy queries
- incompleted query

Term Level Query ( Structured Data)


- term query (rating=8.5)
GET products/_search
{
"_source": ["name", "rating"],
"query": {
  
  "term": {
    "rating": {
      "value": 8.5
    }
  }
}
}
- range query
GET products/_search
{
"_source": ["name", "rating"],
"query": {
  "range": {
    "rating": {
      "gte": 2,
      "lte": 20
    }
  }
}
}
- bool query ( must)
GET products/_search
{
"query": {
  "bool": {
    "must": [
      {"match": {
        "category": "mobile"
      }},
      {
      "range": {
        "prices.usd": {
          "gte": 500,
          "lte": 1000
        }
      }
      }
     
    ]
  }
}
}
- bool query ( not must)
GET products/_search
{
"query": {
  "bool": {
    "must": [{"match": {"category": "mobile"}}],
    "must_not": [{"range": {
      "prices.usd": {
        "lte": 400 //  dont fetch below 400
      }
    }}]
  }
}
}
- bool query ( should) :  the should clause is more about increasing the relevancy score but not affectiong results.
GET products/_search
{
"query": {
  "bool": {
    "must": [{"match": {"category": "mobile"}}],
    "must_not": [{"range": {"prices.usd": {"lte": 200  }}}],
    "should": [
      {
        "match": {
          "name":"Nokia"
        }
      }
    ]
  }
}
}
- bool query (filter)
GET products/_search
{
"query": {
  "bool": {
    "must": [{"match": {"category": "mobile"}}],
    "must_not": [{"range": {"prices.usd": {"lte": 200  }}}],
    "should": [{  "match": {"name":"Nokia"}}],
    "filter": [{"term": {"rating": 9.5}}]
  }
}
}








 
