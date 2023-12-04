'use client';
import React from 'react';
import {variables} from "@/constants/variables";
import {useRouter} from "next/navigation";
import useUser from "@/hooks/useUser";


interface CreateAccountFormProps {
    setIsCreateAccountActive: React.Dispatch<React.SetStateAction<boolean>>;
}

const CreateAccountForm: React.FC<CreateAccountFormProps> = ({ setIsCreateAccountActive }) => {

    const router = useRouter();
    const userContext = useUser();

    const handleCreateAccount = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);
        const accountValues = {
            cpfCnpj: formData.get('cpfCnpj') as string,
            name: formData.get('name') as string,
            email: formData.get('email') as string,
            password: formData.get('password') as string,
        };

       const data = await fetch(variables.API_URL+'/account', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(accountValues)
        })
            .then(response => {
                if (!response.ok || response.status !== 201) {
                    throw response;
                }
                return response.json();
            })
            .catch(async errorResponse => {
                const error = await errorResponse.json();
                console.error('Erro ao criar conta:', error.Message);
            });

            userContext.createAccount(data);
    };

    return (
        <form className="border-2 shadow-lg rounded-sm p-6 space-y-2" onSubmit={handleCreateAccount}>
            <h1 className="flex justify-center">Criar Conta - Psel</h1>
            <div className="space-y-1">
                <label htmlFor="">Cpf/Cnpj</label>
                <input
                    className="border-2 w-full rounded-sm pl-1 border-gray-400 outline-none placeholder:text-xs placeholder:pt-4"
                    type="text"
                    placeholder="Ex: 12345678910"
                    name="cpfCnpj"
                    autoFocus={true}
                />
            </div>
            <div className="space-y-1">
                <label htmlFor="">Nome</label>
                <input
                    className="border-2 w-full rounded-sm pl-1 border-gray-400 outline-none"
                    type="text"
                    name="name"
                />
            </div>
            <div className="space-y-1">
                <label htmlFor="">Email</label>
                <input
                    className="border-2 w-full rounded-sm pl-1 border-gray-400 outline-none"
                    type="email"
                    name="email"
                />
            </div>
            <div className="space-y-1 pb-2">
                <label htmlFor="">Senha</label>
                <input
                    className="border-2 w-full rounded-sm pl-1 border-gray-400 outline-none"
                    type="text"
                    name="password"
                />
            </div>
            <div className="flex justify-between">
                <button className="border-2 rounded-md p-2 border-gray-400 hover:shadow-md hover:shadow-inner hover:font-semibold" type="submit" >Criar Conta</button>
                <button className="border-2 rounded-md p-2 border-gray-400 px-3 hover:shadow-md hover:shadow-inner hover:font-semibold" type="button" onClick={() => setIsCreateAccountActive(false)}>Voltar</button>
            </div>
        </form>
    );
};

export default CreateAccountForm;
