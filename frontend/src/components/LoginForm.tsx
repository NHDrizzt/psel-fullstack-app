import React, {useState} from 'react';
import {variables} from "@/constants/variables";
import {useRouter} from "next/navigation";
import useUser from "@/hooks/useUser";


interface LoginFormProps {
    setIsCreateAccountActive: React.Dispatch<React.SetStateAction<boolean>>;
}


const LoginForm: React.FC<LoginFormProps> = ({ setIsCreateAccountActive }) => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const router = useRouter();
    const userContext = useUser();
    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const response = await fetch(variables.API_URL+"/auth", {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({email, password}),
        });

        if (response.ok) {
            const data = await response.json();
            userContext.login(data);
            await router.push('/profile');
        } else {
            console.error('Erro no login:', response.statusText);
        }
    };

    return (
        <form className="border-2 shadow-lg rounded-sm p-6 space-y-2" onSubmit={handleSubmit}>
            <h1 className="flex justify-center">Login - Psel</h1>
            <div className="space-y-1">
                <label htmlFor="email">Email:</label>
                <input
                    className="border-2 w-full rounded-sm pl-1 border-gray-400 outline-none"
                    type="text"
                    id="email"
                    value={email}
                    autoFocus={true}
                    onChange={(e) => setEmail(e.target.value)}
                />
            </div>
            <div className="space-y-1 pb-2">
                <label htmlFor="password">Senha:</label>
                <input
                    className="border-2 w-full rounded-sm pl-1 border-gray-400 outline-none"
                    type="password"
                    id="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
            </div>
            <div className="flex justify-between">
                <button className="border-2 rounded-md p-2 border-gray-400 px-3 hover:shadow-md hover:shadow-inner hover:font-semibold" type="submit">Entrar</button>
                <button className="border-2 rounded-md p-2 border-gray-400 hover:shadow-md hover:shadow-inner hover:font-semibold" onClick={() => setIsCreateAccountActive(true)}>Criar Conta</button>
            </div>
        </form>
    );
};

export default LoginForm;
