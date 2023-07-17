import { useEffect, useState } from "react"
import { useParams } from "react-router-dom";
import { GetUserWithPosts } from "./PostManager";
import { Card, CardBody, CardImg } from "reactstrap";

export const UserPosts = () => {
    const [userPost, setUserPost] = useState([]);
    const { id } = useParams();

    useEffect(() => {
        GetUserWithPosts(id)
            .then((data) => {
                setUserPost(data)
            })
            .catch((error) => {
                console.log("Error fetching user posts:", error)
            });
    }, [id]);

    return (
        <div className="container">
            {userPost.map((post) => (
                <Card key={post.id}>
                <CardImg top src={post.imageUrl} alt={post.title} />
                <CardBody>
                    <p>{post.title}</p>
                    <p>
                    {post.caption}
                    </p>
                    </CardBody>
                    </Card>         
                ))}
        </div>
    );
}