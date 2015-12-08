create or replace package body userVerifyPK is

--  获取用户头像 url
function getUserHeadImg(email in  varchar2)return  varchar2 is
    -- 获取用户头像url 通过传输来 email 信息 返回用户的头像url
  imgUrl  varchar(50);
begin
      
         select HEADIMAGEURL into imgUrl from  allusers where upper(userEmail) = upper(email) and ISACTIVE = '1';
         return imgUrl;                       -- 若有数据 则返回
exception 
  when no_data_found then
    return null;   -- 若没有数据 则返回是null
   
end getUserHeadImg;



-- 验证用户名密码是否正确
function  verifyLogin(v_userEmail in varchar2 ,v_pwd in varchar2) return varchar2 is
  -- 通过传输来的邮箱和密码验证用户名和密码是否正确 并返回 true 或者 false
  v_count number(1) ;
begin
    
  select count(*) into v_count from allusers where upper(useremail) = upper(v_userEmail) and pwd = v_pwd and ISACTIVE = '1';
  if(v_count = 1) then
  
      return '1';          -- 用户名密码正确 且 用户已经激活
  else
      return '0';
  end if;
end verifyLogin;



-- 检验用户邮箱是否已经存在
function isAlreadyExistsAccount(v_userEmail in varchar2) return varchar2 is
  v_count number(1);
begin
     
          select count(*) into v_count  from allusers where upper(userEmail) = upper(v_userEmail) and ISACTIVE = '1';
          if(v_count = 1) then
               return '1';                       -- 存在 则返回 '1'
           else
               return '0';                 --  不存在则返回 '0'
                
          end if;
       
end isAlreadyExistsAccount;



-- 在注册页面 所需要插入的用户的基本信息
function insertUserBaseInfo(v_userEmail in varchar2,v_pwd in varchar2,first_name in varchar2,last_name in varchar) return varchar2 is
  headerImgUrl  varchar2(50) := '../../UserHeadImg/default.png';
 begin
   delete from allusers where userEmail = v_userEmail;
   insert into allusers (userEmail,pwd,firstname,lastname,createTime,headimageurl,isactive) values(v_userEmail,v_pwd,first_name,last_name,sysdate,headerImgUrl,'0');
   commit;
   return '1';
 exception 
   when others then
     rollback;
     return '0';
 end insertUserBaseInfo;
 
 
 -- 为用户激活,若用户未激活,则激活并返回1 成功 。否则返回 0 失败
   function userActive(v_userEmail in varchar2) return varchar2 is
        v_count number(1) ;
   begin
        select count(*) into v_count from allusers where upper(userEmail) = upper(v_userEmail) and isActive = '0';
        if(v_count = 1) then              -- 未激活,且已经注册
            update allusers set isActive = '1' where userEmail = v_userEmail ;
            commit;
            return '1'; 
        else          -- 不存该用户 或已经激活
            return '0';
        end if;
   end userActive;
 
end;
/
