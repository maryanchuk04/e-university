import NextAuth from 'next-auth';
import GoogleProvider from 'next-auth/providers/google';

export const authOptions = {
    providers: [
        GoogleProvider({
            clientId: process.env.GOOGLE_OAUTH_CLIENT_ID ?? '',
            clientSecret: process.env.GOOGLE_OAUTH_CLIENT_SECRET ?? '',
        }),
    ],
    callbacks: {
        async signIn(params: any) {
            const { profile  } = params;
            try {
                const res = await fetch(`${process.env.GATEWAY_BASE_ADDRESS}/api/authenticate`, { method: 'POST' });
                if (!res.ok) {
                    return false;
                }

				await res.json();
				

                return true;
            } catch (err) {
                console.log('Something happened: ', err);
                return false;
            }
        },
    },
};

const handler = NextAuth(authOptions);
export { handler as GET, handler as POST };
