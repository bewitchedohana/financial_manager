import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, FormProvider } from "react-hook-form";
import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./form";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button } from "./button";
import z from "zod";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";
import { motion } from "motion/react";

type FormTypes = {
  onSubmit: (values: z.infer<typeof SignUpFormSchema>) => Promise<void>;
  onFormChange: () => void;
};

export const SignUpFormSchema = z.object({
  firstName: z.string().min(1, "Name is required"),
  lastName: z.string().min(1, "Last name is required"),
  email: z.string().email(),
  password: z.string().min(16),
});

export const SignUpForm = ({ onSubmit, onFormChange }: FormTypes) => {
  const signUpForm = useForm<z.infer<typeof SignUpFormSchema>>({
    resolver: zodResolver(SignUpFormSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
      password: "",
    },
  });

  return (
    <motion.div
      exit={{ opacity: 0, transform: "translateX(-100%)" }}
      className="border-1 rounded-2xl bg-white shadow-lg min-w-80 p-6"
    >
      <FormProvider {...signUpForm}>
        <form onSubmit={signUpForm.handleSubmit(onSubmit)}>
          <FormField
            control={signUpForm.control}
            name="firstName"
            render={({ field }) => (
              <FormItem className="mb-4">
                <FormLabel>Name</FormLabel>
                <FormMessage />
                <FormControl>
                  <input
                    placeholder="Name"
                    {...field}
                    className="border-1 p-1 rounded-md border-black"
                  />
                </FormControl>
              </FormItem>
            )}
          />

          <FormField
            control={signUpForm.control}
            name="lastName"
            render={({ field }) => (
              <FormItem className="mb-4">
                <FormLabel>Name</FormLabel>
                <FormMessage />
                <FormControl>
                  <input
                    placeholder="Name"
                    {...field}
                    className="border-1 p-1 rounded-md border-black"
                  />
                </FormControl>
              </FormItem>
            )}
          />

          <FormField
            control={signUpForm.control}
            name="email"
            render={({ field }) => (
              <FormItem className="mb-4">
                <FormLabel>Email</FormLabel>
                <FormMessage />
                <FormControl>
                  <input
                    placeholder="Email"
                    {...field}
                    className="border-1 p-1 rounded-md border-black"
                  />
                </FormControl>
              </FormItem>
            )}
          />

          <FormField
            control={signUpForm.control}
            name="password"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Password</FormLabel>
                <FormMessage />
                <FormControl>
                  <input
                    type="password"
                    placeholder="Password"
                    {...field}
                    className="border-1 p-1 rounded-md border-black"
                  />
                </FormControl>
              </FormItem>
            )}
          />

          <div className="flex justify-end">
            <button
              className="text-xs cursor-pointer my-2"
              type="button"
              onClick={onFormChange}
            >
              Sign in instead
              <FontAwesomeIcon icon={faArrowRight} className="ml-1" />
            </button>
          </div>

          <div className="flex justify-end mt-2">
            <Button
              className="ml-auto cursor-pointer bg-dn-orange hover:bg-yellow-400 text-black min-w-24"
              type="submit"
            >
              Sign up
            </Button>
          </div>
        </form>
      </FormProvider>
    </motion.div>
  );
};
