-------------------------------------------Triggers da Tabela COLISAO ------------------------------------------------------------
CREATE or alter TRIGGER trg_CRUD_Colisao on Colisao
for insert,delete,update
as
IF (SELECT COUNT(*)
	FROM inserted) >= 1 AND (select count(*)
	from deleted) <> 0
	begin
		print('Os seguintes dados foram modificados')
		select * from deleted
		select * from inserted
	end
	else if(SELECT COUNT(*)
			FROM inserted) >= 1
begin
		PRINT('Colisão inserido com sucesso!')
end
else if (select count(*)
		from deleted ) >= 1
	begin
			print('Lançamento Removido !')
	end



-------------------------------------------Triggers da Tabela TIRO ------------------------------------------------------------

CREATE or alter TRIGGER trg_CRUD_Tiro on Tiro
for insert,delete,update
as
IF (SELECT COUNT(*)
	FROM inserted) >= 1 AND (select count(*)
	from deleted) <> 0
	begin
		print('Os seguintes dados foram modificados')
		select * from deleted
		select * from inserted
	end
	else if(SELECT COUNT(*)
			FROM inserted) >= 1
begin
		PRINT('Colisão inserido com sucesso!')
end
else if (select count(*)
		from deleted ) >= 1
	begin
			print('Lançamento Removido !')
	end


-------------------------------------------Triggers da tabela METEORO ------------------------------------------------------------
CREATE or alter TRIGGER trg_CRUD_Meteoro on Meteoro
for insert,delete,update
as
IF (SELECT COUNT(*)
	FROM inserted) >= 1 AND (select count(*)
	from deleted) <> 0
	begin
		print('Os seguintes dados foram modificados')
		select * from deleted
		select * from inserted
	end
	else if(SELECT COUNT(*)
			FROM inserted) >= 1
begin
		PRINT('Colisão inserido com sucesso!')
end
else if (select count(*)
		from deleted ) >= 1
	begin
			print('Lançamento Removido !')
	end







	---------------------------

	IF (SELECT COUNT(*)
	FROM inserted) >= 1 AND (select count(*)
	from deleted) <> 0
	begin
		print('Os seguintes dados foram modificados')
		declare @codColisao int
		declare @alturaColisao decimal
		declare @distanciaColisao decimal
		set @codColisao = (select CodColisao from deleted)
		set @alturaColisao = (select alturaColisao from deleted)
		set @distanciaColisao = (select distanciaColisao from deleted)

		print ('CodColisao: ' + cast(@codcolisao as varchar) + 'º Já está cadastrado!')


		select * from inserted
	end
	else if(SELECT COUNT(*)
			FROM inserted) >= 1
begin
		PRINT('Colisão inserido com sucesso!')
end
else if (select count(*)
		from deleted ) >= 1
	begin
			print('Lançamento Removido !')
	end