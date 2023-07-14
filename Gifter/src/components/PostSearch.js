import React, { useState } from "react";
import { searchPosts } from "./PostManager";


    export const PostSearch = () => {

        const [ searchTerm, setSearchTerm] = useState("")
        const [ searchResults, setSearchResults ] = useState({})
     
        const handleSearchInput = (event) => {
            setSearchTerm(event.target.value)
        };
    
        const handleButtonClick = (event) => {
            event.preventDefault();
    
            searchPosts(searchTerm)
            .then((res) => {
                setSearchResults(res);
        })}
            
        return (
    <div>
    <form className="post-form">
      <div>
        <label htmlFor = "searchTerm">Search:</label>
        <input
          type="text"
          id="searchTermy"
          value={searchTerm}
          onChange={handleSearchInput}
        />
      </div>
      <button onClick={(clickEvent) => handleButtonClick(clickEvent)}
            className="btn btn-primary">
        Search
      </button>
    </form>
    {searchResults.length > 0 && (
      <div>
        <h3>Search Results:</h3>
        <div>
          {searchResults.map((post) => (
            <div key={post.id}>
              <h4>{post.title}</h4>
              <p>Caption: {post.caption}</p>
              <img src={post.imageUrl} alt={post.title} />
            </div>
          ))}
         </div>
    </div>
    )}
    </div>
        )}