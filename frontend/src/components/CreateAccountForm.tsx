'use client';
import React, {useState} from 'react';
import {variables} from "@/constants/variables";
import {useRouter} from "next/navigation";
import useUser from "@/hooks/useUser";
import {Simulate} from "react-dom/test-utils";
import error = Simulate.error;

interface ApiError {
    StatusCode: number;
    Message: string;
    ErrorType: string;
}

interface ErrorBackend {
    EmailExists?: string;
    CpfCnpjExists?: string;
    NameExists?: string;
}


interface CreateAccountFormProps {
    setIsCreateAccountActive: React.Dispatch<React.SetStateAction<boolean>>;
}

interface ErrorKeyMap {
    [key: string]: keyof ErrorBackend;
}
const CreateAccountForm: React.FC<CreateAccountFormProps> = ({ setIsCreateAccountActive }) => {
    const [errors, setErrors] = useState<ErrorBackend>({});

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

        let isSuccess = false;

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
           .catch(async (error) => {
               const errorStream = await error.json();
               let errorObject: { [key: string]: string } = {};
               errorStream.forEach((error: ApiError) => {
                   errorObject[error.ErrorType] = error.Message;
               })
               setErrors(errorObject)
           })
        userContext.createAccount(data);
        isSuccess = true;
        setErrors(errors => {
            if (isSuccess && Object.values(errors).every(val => val === "")) {
                setIsCreateAccountActive(false);
                return errors;
            }
            return errors;
        });
    };

    const errorKeyMap: ErrorKeyMap  = {
        email: 'EmailExists',
        cpfCnpj: 'CpfCnpjExists',
        name: 'NameExists'
    };


    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const {name, value} = e.target;

        const errorKey = errorKeyMap[name];
        if (errorKey) {
            setErrors({...errors, [errorKey]: ''});
        }}

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
                    onChange={handleChange}
                />
                {errors?.CpfCnpjExists && <p className="text-red-500 text-xs">{errors.CpfCnpjExists}</p>}
            </div>
            <div className="space-y-1">
                <label htmlFor="">Nome</label>
                <input
                    className="border-2 w-full rounded-sm pl-1 border-gray-400 outline-none"
                    type="text"
                    name="name"
                    onChange={handleChange}
                />
                {errors?.NameExists && <p className="text-red-500 text-xs">{errors.NameExists}</p>}

            </div>
            <div className="space-y-1">
                <label htmlFor="">Email</label>
                <input
                    className="border-2 w-full rounded-sm pl-1 border-gray-400 outline-none"
                    type="email"
                    name="email"
                    onChange={handleChange}
                />
                {errors?.EmailExists && <p className="text-red-500 text-xs">{errors.EmailExists}</p>}
            </div>
            <div className="space-y-1 pb-2">
                <label htmlFor="">Senha</label>
                <input
                    className="border-2 w-full rounded-sm pl-1 border-gray-400 outline-none"
                    type="password"
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
