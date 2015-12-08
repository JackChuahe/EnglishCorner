create or replace package body communityOperation is
       --��ȡָ����������Ϣ
     function getCommunityInfo (v_communityID in varchar2) return sys_refcursor is
        my_cursor sys_refcursor;
     begin
        open my_cursor for select communityName,COMMUNITYDESC,COMMUNITYHEADIMAGEURL from communitys where communityID = v_communityID;
        
        return my_cursor;
     
     end getCommunityInfo;
    

--
 -- ��ȡָ�������Ķ���û�ͷ��
       function getCommunityUsersHeadImg(v_communityID in varchar2) return sys_refcursor is
                my_cursor sys_refcursor;
        begin
                open my_cursor for select HEADIMAGEURL from allusers where exists( select * from user_community where communityID =v_communityID and user_community.useremail = allusers.useremail );
                return my_cursor;
        end getCommunityUsersHeadImg;

  --- ����ָ����������ȡ��������������ж�̬���� �� ������ ������ʱ�� �����ߵ�ͷ��url
 function getCommunityAllContents(v_communityID in varchar2) return sys_refcursor is
   my_cursor sys_refcursor;
 begin
    open my_cursor for select  usersContents.Contentid,usersContents.Text,usersContents.Textpicurl,to_char(usersContents.Contentsubmitdate,'yyyy-MM-DD HH24:mi:ss'),(select HEADIMAGEURL from allusers where userEmail = usersContents.useremail) userHeadImage,(select (firstname || ' '||lastname) from allusers where userEmail = usersContents.useremail) userName from usersContents where usersContents.Communityid = v_communityID order by usersContents.Contentsubmitdate desc;
    return my_cursor;
 end getCommunityAllContents;
 
 
 --  -- ͨ��contentID��ȡÿ��content���������ۼ����۵��˺��˵�ͷ��
function GetContentComments(v_contentID in varchar2) return sys_refcursor  
is
 my_cursor sys_refcursor;
begin
  
       open my_cursor for select (select HEADIMAGEURL from allusers where allusers.useremail = comments.useremail) userHeadImg,(select firstname || ' ' || lastname from allusers where allusers.useremail = comments.useremail) userName ,to_char(comments.commentdate,'yyyy-MM-DD HH24:mi:ss') submitTime,comments.commentcontent from comments where comments.contentid = v_contentID order by comments.commentdate ;
       return my_cursor;
end GetContentComments;


--ͨ���ô���Ķ�ĳ��content��������Ϣ ���� contentID��userEmail������text, ������ 1 ���� 0
 function insertAComment(v_contentID in varchar2,v_userEmail in varchar2,v_text in varchar2) return varchar2 is
 begin
       insert into comments values( 'comt'|| to_char(systimestamp,'mmDDHH24missff6'),v_contentID,v_userEmail,v_text ,sysdate);
       commit;
      return '1';   --- �����ɹ�
 exception 
   when dup_val_on_index then
     rollback;
   return '0';       -- ����ʧ��
 end insertAComment ;
 
 
        
  --- �����û�����Ķ�̬,����userEmail  communityID  text   picUrl ���в���
  -- ����ɹ�����1 ʧ�ܷ���0
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
