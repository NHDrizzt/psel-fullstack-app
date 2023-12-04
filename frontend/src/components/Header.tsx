import React, {useState} from 'react';
import useUser from "@/hooks/useUser";

const Header = () => {

    const { user } = useUser();
    const [data , setData] = useState(user);


    return (
        <div className="border-2 p-6">
            {
                data && (
                    <div>
                        <h1>Bem vindo, {data.name}</h1>
                        <p>{}</p>
                    </div>
                )
            }
        </div>
    );
};

export default Header;
