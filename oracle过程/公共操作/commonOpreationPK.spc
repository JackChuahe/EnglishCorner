create or replace package commonOpreationPK is
       /*
           公共操作的包,几乎所有页面都会进行访问的包和数据
       */
       function getHeadImgAndUserEmail(v_userEmail in varchar2,v_lastname out varchar2) return varchar2;          -- 传入用户邮箱,若用户存在则返回用户头像的url 否则为null
end;
/
