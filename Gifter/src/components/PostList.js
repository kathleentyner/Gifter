import React, { useContext, useEffect, useState } from "react";
import { getAllPosts, getAllPostsWithComments } from "./PostManager";
import { Post } from "./Post";

export const PostList = () => {
  const [posts, setPosts] = useState([]);

  const getPosts = () => {
    getAllPostsWithComments().then(allPosts => setPosts(allPosts)); 
  };

  useEffect(() => {
    getPosts();
  }, []);

  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="cards-column">
          {posts.map((post) => (
            <Post key={post.id} post={post} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default PostList;