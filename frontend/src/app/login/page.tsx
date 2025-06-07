"use client";

import { Button } from "@/components/ui/button";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage
} from "@/components/ui/form";
import { post } from "@/lib/apiClient";
import { zodResolver } from "@hookform/resolvers/zod";
import { BaseSyntheticEvent } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";

const loginFormSchema = z.object({
  email: z.string().email(),
  password: z.string().min(16),
});

export default function Page() {
  const loginForm = useForm<z.infer<typeof loginFormSchema>>({
    resolver: zodResolver(loginFormSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const loginFormSubmit = async (values: z.infer<typeof loginFormSchema>) => {
    try {
        const response = await post('/auth', values);
        console.log(response);
    } catch (error) {
        console.error(error);
    }
  };

  return (
    <main>
      <section className="flex">
        <div className="border-1 p-3 rounded-2xl">
          <Form {...loginForm}>
            <form onSubmit={(e) => {e.preventDefault();loginForm.handleSubmit(loginFormSubmit)}}>
              <FormField
                control={loginForm.control}
                name="email"
                render={({ field }) => (
                  <FormItem className="mb-4">
                    <FormLabel>Email</FormLabel>
                    <FormMessage />
                    <FormControl>
                      <input placeholder="Email" {...field} />
                    </FormControl>
                  </FormItem>
                )}
              />

              <FormField
                control={loginForm.control}
                name="password"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Password</FormLabel>
                    <FormMessage />
                    <FormControl>
                      <input type="password" placeholder="Password" {...field} />
                    </FormControl>
                  </FormItem>
                )}
              />

                <div className="flex justify-end mt-2">
                    <Button className="ml-auto" type="submit">Login</Button>
                </div>
            </form>
          </Form>
        </div>
      </section>

      <section></section>
    </main>
  );
}
