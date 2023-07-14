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
