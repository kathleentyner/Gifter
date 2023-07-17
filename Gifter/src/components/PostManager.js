import React from "react";

const baseUrl = '/api/post';

export const getAllPosts = () => {
  return fetch(baseUrl) 
    .then((res) => res.json())
};

export const addPost = (singlePost) => { 
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(singlePost),
  });
};

export const searchPosts = (query)=> { 
  return fetch(`${baseUrl}/search?q=${query}`)
  .then((res)=> res.json())
};

export const getAllPostsWithComments = () => {
  return fetch(`${baseUrl}/GetWithComments`) 
    .then((res) => res.json())
};
//get post by id
export const getPost = (id) => {
  return fetch(`/api/post/${id}`).then((res) => res.json());
};


export const GetUserWithPosts = (id) => {
    return fetch(`${baseUrl}/GetUserWithPosts/${id}/posts`)
        .then((res) => res.json())
}