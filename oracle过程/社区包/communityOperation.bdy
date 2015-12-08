create or replace package body communityOperation is
       --获取指定社区的信息
     function getCommunityInfo (v_communityID in varchar2) return sys_refcursor is
        my_cursor sys_refcursor;
     begin
        open my_cursor for select communityName,COMMUNITYDESC,COMMUNITYHEADIMAGEURL from communitys where communityID = v_communityID;
        
        return my_cursor;
     
     end getCommunityInfo;
    

--
 -- 获取指定社区的多个用户头像
       function getCommunityUsersHeadImg(v_communityID in varchar2) return sys_refcursor is
                my_cursor sys_refcursor;
        begin
                open my_cursor for select HEADIMAGEURL from allusers where exists( select * from user_community where communityID =v_communityID and user_community.useremail = allusers.useremail );
                return my_cursor;
        end getCommunityUsersHeadImg;

  --- 传入指定社区，获取该社区发表的所有动态内容 和 发布者 及发布时间 发布者的头像url
 function getCommunityAllContents(v_communityID in varchar2) return sys_refcursor is
   my_cursor sys_refcursor;
 begin
    open my_cursor for select  usersContents.Contentid,usersContents.Text,usersContents.Textpicurl,to_char(usersContents.Contentsubmitdate,'yyyy-MM-DD HH24:mi:ss'),(select HEADIMAGEURL from allusers where userEmail = usersContents.useremail) userHeadImage,(select (firstname || ' '||lastname) from allusers where userEmail = usersContents.useremail) userName from usersContents where usersContents.Communityid = v_communityID order by usersContents.Contentsubmitdate desc;
    return my_cursor;
 end getCommunityAllContents;
 
 
 --  -- 通过contentID获取每该content的所有评论及评论的人和人的头像
function GetContentComments(v_contentID in varchar2) return sys_refcursor  
is
 my_cursor sys_refcursor;
begin
  
       open my_cursor for select (select HEADIMAGEURL from allusers where allusers.useremail = comments.useremail) userHeadImg,(select firstname || ' ' || lastname from allusers where allusers.useremail = comments.useremail) userName ,to_char(comments.commentdate,'yyyy-MM-DD HH24:mi:ss') submitTime,comments.commentcontent from comments where comments.contentid = v_contentID order by comments.commentdate ;
       return my_cursor;
end GetContentComments;


--通过用传入的对某条content的评论信息 包括 contentID，userEmail，评论text, 并返回 1 或者 0
 function insertAComment(v_contentID in varchar2,v_userEmail in varchar2,v_text in varchar2) return varchar2 is
 begin
       insert into comments values( 'comt'|| to_char(systimestamp,'mmDDHH24missff6'),v_contentID,v_userEmail,v_text ,sysdate);
       commit;
      return '1';   --- 操作成功
 exception 
   when dup_val_on_index then
     rollback;
   return '0';       -- 操作失败
 end insertAComment ;
 
 
        
  --- 插入用户发表的动态,传入userEmail  communityID  text   picUrl 进行插入
  -- 插入成功返回1 失败返回0
 function insertUserContent(v_userEmail in varchar2,v_communityID in varchar2,v_text in varchar2,v_picUrl in varchar2)return varchar2 is
          
  begin
          insert into userscontents values('cont'||to_char(systimestamp,'MMDDHH24miSSff6'),v_userEmail,v_communityID,v_text,v_picUrl,sysdate);
          commit;
          return '1';
  exception
 when dup_val_on_index then
   rollback;
   return '0';
 end insertUserContent;
 
end;
/
