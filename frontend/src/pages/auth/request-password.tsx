import React from 'react';
import { Button } from '@paljs/ui/Button';
import { InputGroup } from '@paljs/ui/Input';
import Link from 'next/link';

import Layout from '../../Layout';
import Auth, { Group } from '../../components/Auth';

export default function RequestPassword() {
  return (
    <Layout title="a">
      <Auth title="Forgot Password" subTitle="Vui lòng nhập Email để tạo lại mật khẩu.">
        <form>
          <InputGroup fullWidth>
            <input type="email" placeholder="Địa chỉ Email" />
          </InputGroup>
          <Button status="Success" type="button" shape="SemiRound" fullWidth>
            Tạo lại mật khẩu
          </Button>
        </form>
        <Group>
          <Link href="/auth/login">
            <a>Đăng nhập</a>
          </Link>
          <Link href="/auth/register">
            <a>Đăng ký</a>
          </Link>
        </Group>
      </Auth>
    </Layout>
  );
}
