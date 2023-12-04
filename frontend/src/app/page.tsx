'use client';
import React, {useContext, useState} from 'react';
import {variables} from "@/constants/variables";
import Link from "next/link";
import {useRouter} from "next/navigation";
import useUser from "@/hooks/useUser";
import CreateAccountForm from "@/components/CreateAccountForm";
import LoginForm from "@/components/LoginForm";

export default function Login() {

  const [isCreateAccountActive, setIsCreateAccountActive] = useState(false);

  return (
      <main className="grid place-items-center h-screen">
        {
          isCreateAccountActive ? (
              <CreateAccountForm setIsCreateAccountActive={setIsCreateAccountActive} />
          ) : (
              <LoginForm setIsCreateAccountActive={setIsCreateAccountActive} />
          )
        }




      </main>
  );
}
