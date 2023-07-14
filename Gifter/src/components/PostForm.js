import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom"
import { addPost } from "./PostManager"

export const PostForm = () => {
  const [post, update] = useState({
    title: "",
    imageUrl: "",
    caption: "",
    userProfileId: 1,
    dateCreated: Date.now()
})
const navigate = useNavigate()
const handleSaveButtonClick = (event) => {
        event.preventDefault()
        const postToAPI = {
            Title: post.title,
            Caption: post.caption,
            ImageUrl: post.imageUrl,
            DateCreated: new Date().toISOString(),
            UserProfileId: 1,
        };
    return addPost(postToAPI)
        .then(navigate("/"));
};


  return (
    <form className="postForm">
        <h2 className="postForm__title">New Post</h2>
        <fieldset>
            <div className="form-group">
                <label htmlFor="title">Title:</label>
                <input
                    required autoFocus
                    type="text"
                    className="form-control"
                    placeholder="Title of post"
                    value={post.title}
                    onChange={ 
                        (event) => {
                        const copy = {...post} //make a copy of the ticket
                        copy.title = event.target.value //look at the state of the ticket description
                        update(copy)
                    } 
                }/>
            </div>
        </fieldset>
        <fieldset>
            <div className="form-group">
                <label htmlFor="imageUrl">URL:</label>
                <input  
                    required autoFocus
                    type="text"
                    className="form-control"
                    value={[post.imageUrl]}
                    onChange={
                        (event) => {
                            const copy = {...post}
                            copy.imageUrl = event.target.value
                            update(copy)  // send the new state back

                        }
                    } />
            </div>
        </fieldset>
        <fieldset>
            <div className="form-group">
                <label htmlFor="caption">Caption:</label>
                <input 
                required autoFocus
                type="text"
                className="form-control"
                    value={[post.caption]}
                    onChange={
                        (event) => {
                            const copy = {...post}
                            copy.caption = event.target.value
                            update(copy)  // send the new state back

                        }
                    } />
            </div>
        </fieldset>
  
        <button 
        onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
            className="btn btn-primary">
            Submit
        </button>
    </form>
  )
 }